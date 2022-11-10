using System.Linq;
using Core;
using Core.Data.Provider;
using Entitas;
using GameKit;
using RoyalAxe.Configs;
using RoyalAxe.CoreLevel;
using RoyalAxe.Units;
using RoyalAxe.Units.Player;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public class UnitsViewBuilder : IUnitsViewBuilder
    {
        private readonly IDataStorage _dataStorage;
        private readonly IGroup<CoreGamePlayEntity> _allChunksGroup;
        private readonly IUltimateCheatAdapter _ultimateCheatSettings;
        private readonly ILevelPositionCalculation _levelPositionCalculation;

        public UnitsViewBuilder(IDataStorage dataStorage,
                                IContext<CoreGamePlayEntity> coreGamePlayContext,
                                IUltimateCheatAdapter ultimateCheatSettings,
                                ILevelPositionCalculation levelPositionCalculation)
        {
            _dataStorage = dataStorage;
            _ultimateCheatSettings = ultimateCheatSettings;
            _levelPositionCalculation = levelPositionCalculation;
            _allChunksGroup = coreGamePlayContext.GetGroup(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.ChunkView));
        }

        public UnitsView BuildPlayerView(UnitsEntity unitsEntity)
        {
            var playerHeroConfig = _dataStorage.First<PlayerCharacterConfigDef>();
            return TryBuild<PlayerUnitView, PlayerCharacterConfigDef>(playerHeroConfig.UniqueID,unitsEntity);
        }

        public BosonView BuildBosonView(UnitsEntity boson, UnitConfigDef bosonViewConfig, Vector3 pos)
        {
            if (bosonViewConfig.Prefab is BosonView bosonView)
            {
                BosonView view = Build(bosonView, boson);
                boson.AddSpriteRender(view.SpriteRenderer);
                view.RootTransform.position = pos;
                return view;
            }

            HLogger.LogError($"{bosonViewConfig.UniqueID} prefab is not {nameof(BosonView)}");
            return null;
        }

        public WizardShopUnitView BuildWizardView(UnitsEntity wizardShop)
        {
            var npcUnitConfigDef = _dataStorage.First<NPCUnitConfigDef>();
            var view = TryBuild<WizardShopUnitView, NPCUnitConfigDef>(npcUnitConfigDef.UniqueID, wizardShop);

            if (view != null)
            {
                var pos = _levelPositionCalculation.CalcWizardPosition(view);
                view.RootTransform.position = pos;
            }

            return view;
        }

        public UnitsView BuildMobView(UnitsEntity unitsEntity, Vector2 pos)
        {
            var view = BuildEnemyView(unitsEntity, unitsEntity.unit.Id);
            view.RootTransform.position = pos;
            var chunk = _allChunksGroup.AsEnumerable().FirstOrDefault(o => o.chunkBounds.Contains(pos));

            if (chunk != null)
            {
                view.RootTransform.SetParent(chunk.chunkView.RootTransform, true);
            }

            return view;
        }

        private UnitsView BuildEnemyView(UnitsEntity unit, string viewId)
        {
            return TryBuild<UnitsView, UnitConfigDef>(viewId, unit);
        }

        private TUnitsView TryBuild<TUnitsView, TConfig>(string viewId, UnitsEntity unit)
            where TConfig : BaseUnitConfig where TUnitsView : BaseUnitView
        {
            var unityConfig = _dataStorage.ById<TConfig>(viewId);

            if (unityConfig == null)
            {
                HLogger.LogError($"нет Конфига юнита {viewId}");
                return null;
            }

            if (unityConfig.Prefab == null)
            {
                HLogger.LogError($"нет префаба юнита {viewId}");
                return null;
            }

            if (unityConfig.Prefab is TUnitsView unitView)
            {
                return Build(unitView, unit);
            }

            HLogger.LogError($"{unityConfig.UniqueID} prefab is not {nameof(UnitsView)}");
            return null;
        }

        private T Build<T>(T prefab, UnitsEntity e) where T : BaseUnitView
        {
            var result = Object.Instantiate(prefab);
            result.EntityBehaviours().Where(o => null != o).ForEach(b => b.InitEntity(e));
            e.AddUnitsView(result);

            if (!_ultimateCheatSettings.EnableRender) result.GetComponentsInChildren<Renderer>().ForEach(r => r.enabled = false);
            return result;
        }
    }
}
