using System;
using Core.Data.Provider;
using Entitas;
using RoyalAxe.Units.Stats;
using RoyalAxe.GameEntitas;
using RoyalAxe.Units.Mob;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class DefaultGunnerSkillExecuteSystem : UpdateUsagesSystem, IInitializeSystem
    {
        private readonly UnitsContext _unitsContext;
        private readonly IDataStorage _dataStorage;
        private readonly IBosonUnitPipeline _bosonUnitPipeline;
        private readonly IUnitsEntityFactory _unitsEntityFactory;

        private GunnerMobSkillSettings _skillSettings;

        public DefaultGunnerSkillExecuteSystem(IContext<SkillEntity> context,
                                               UnitsContext unitsContext,
                                               IDataStorage dataStorage,
                                               IBosonUnitPipeline bosonUnitPipeline,
                                               IUnitsEntityFactory unitsEntityFactory) : base(context)
        {
            _unitsContext = unitsContext;
            _dataStorage = dataStorage;
            _bosonUnitPipeline = bosonUnitPipeline;
            _unitsEntityFactory = unitsEntityFactory;
        }

        protected override ICollector<SkillEntity> GetTrigger(IContext<SkillEntity> context)
        {
            return context.CreateCollector(SkillsMatcherLibrary.UsingSkillMather(SkillMatcher.GunnerMobSkill).Added());
        }

        public void Initialize()
        {
            _skillSettings = _dataStorage.ById<SkillsSettings>(GunnerMobSkillSettings.SKILL_ID) as GunnerMobSkillSettings;
        }

        protected override void DoSkillAction(SkillEntity skill)
        {
            // получаем данные для активации скила
            // Позицию для спавна ядра пушки
            // создаем unit сущность ядра пушки на основе пушечника
            // создаем вьюшку ядра - биндим туда сущность ядра
            // Выключаем скилл

            UnitsEntity gunner = skill.gunnerMobSkill.Owner;
            var mobView = gunner.unitsView.View as GunnerMobView;

            if (mobView == null)
            {
                throw new ArgumentException($"{gunner.unit.Id} view need {nameof(GunnerMobView)} view");
            }

            var playerPosition = _unitsContext.playerEntity.unitsView.RootTransform.position;
            var boson = _unitsEntityFactory.CreateEnemyMobBoson(gunner);
            skill.ReplaceMovingToPoint(new SimpleVector2Adapter(playerPosition));
            _bosonUnitPipeline.CreateBosonInWorld(skill, boson, _skillSettings, mobView.CannonBallSpawn);

            skill.isSkillUse = false;
        }
    }
}
