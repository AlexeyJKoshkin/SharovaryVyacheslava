using System.Linq;
using Core;
using Core.Data.Provider;
using Entitas;
using GameKit;
using RoyalAxe.Configs;
using RoyalAxe.Units;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public class UnitsViewBuilder : IUnitsViewBuilder
    {
        private readonly IDataStorage _dataStorage;
        private readonly IGroup<CoreGamePlayEntity> _allChunksGroup;

        public UnitsViewBuilder(IDataStorage dataStorage, IContext<CoreGamePlayEntity> coreGamePlayContext)
        {
            _dataStorage    = dataStorage;
            _allChunksGroup = coreGamePlayContext.GetGroup(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.ChunkView));
        }

        public UnitsView BuildPlayerView(UnitsEntity unitsEntity)
        {
            var playerHeroConfig = _dataStorage.First<PlayerCharacterConfigDef>();
            if (playerHeroConfig == null)
            {
                HLogger.LogError($"нет Конфига игрока {unitsEntity.unit.Id}");
                return null;
            }

            if (playerHeroConfig.Prefab == null)
            {
                HLogger.LogError($"нет префаба игрока {unitsEntity.unit.Id}");
                return null;
            }

            return Build(playerHeroConfig.Prefab, unitsEntity) as UnitsView;
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
            var unityConfig = _dataStorage.ById<UnitConfigDef>(viewId);
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

            if (unityConfig.Prefab is UnitsView unitView)
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
            result.GetComponentsInChildren<Renderer>().ForEach(r=> r.enabled = false);
            return result;
        }
    }
}