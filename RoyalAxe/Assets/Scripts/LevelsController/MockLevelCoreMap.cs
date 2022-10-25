using System.Collections;
using System.Collections.Generic;
using Core;
using Entitas;
using GameKit;
using RoyalAxe.GameEntitas;
using RoyalAxe.Map;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class MockLevelCoreMap : IRoyalAxeCoreMap, IEnemyWaveGenerator
    {
        public int CurrentMobAmount => _allMobs.count; //Количество текущих мобов на уровне
        private readonly IMobPositionGenerator _generator;
        private readonly IGroup<UnitsEntity> _allMobs;
        private readonly IUnitsBuilderFacade _unitsBuilder;
        private readonly ILevelWaveProvider _levelWaveProvider;

        public MockLevelCoreMap(UnitsContext unitsContext,
                                IUnitsBuilderFacade unitsBuilder,
                                IMobPositionGenerator mobPositionGenerator,
                                ILevelWaveProvider levelWaveProvider)
        {
            _unitsBuilder = unitsBuilder;

            _allMobs = unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.Mob, UnitsMatcher.UnitsView)
                                                                 .NoneOf(UnitsMatcher.DeadUnit, UnitsMatcher.Boson, UnitsMatcher.DestroyUnit));

            _generator = mobPositionGenerator;
            _levelWaveProvider = levelWaveProvider;
        }

        IEnemyWaveGenerator IRoyalAxeCoreMap.StartGenerateMobPosition()
        {
            _generator.Reset();
            return this;
        }

        void IEnemyWaveGenerator.GenerateEnemy(string modDataMobId, byte modDataMobLevel)
        {
            var mobReward = _levelWaveProvider.CurrentMobReward;
            var pos = _generator.GetPosForNewMob(modDataMobId);
            var entity = _unitsBuilder.CreateEnemyMobUnit(modDataMobId, modDataMobLevel, pos.startPoint);

            if (!pos.endPoint.Equals(pos.startPoint))
            {
                entity.ReplaceMovingToPoint(new SimpleVector2Adapter(pos.endPoint));
            }

            entity.AddMobDeathReward(mobReward.Expa, mobReward.Gold, mobReward.Gems);
        }
    }
}
