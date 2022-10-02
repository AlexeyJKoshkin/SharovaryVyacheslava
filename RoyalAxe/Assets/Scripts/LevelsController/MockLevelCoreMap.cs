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


        private IMobPositionGenerator _generator;


        private readonly IGroup<UnitsEntity> _allMobs;
        private readonly IUnitsBuilderFacade _unitsBuilder;

        public MockLevelCoreMap(UnitsContext unitsContext, IUnitsBuilderFacade unitsBuilder, IMobPositionGenerator mobPositionGenerator)
        {
            _unitsBuilder = unitsBuilder;
            _allMobs = unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.Mob, UnitsMatcher.UnitsView)
                                                                 .NoneOf(UnitsMatcher.DeadUnit, UnitsMatcher.Boson, UnitsMatcher.DestroyUnit));
            _generator = mobPositionGenerator;
        }

        IEnemyWaveGenerator IRoyalAxeCoreMap.StartGenerateMobPosition()
        {
            _generator.Reset();
            return this;
        }


        void IEnemyWaveGenerator.GenerateEnemy(string modDataMobId, byte modDataMobLevel, MobDeathReward mobReward)
        {
            var pos    = _generator.GetPosForNewMob(modDataMobId);
            var entity = _unitsBuilder.CreateEnemyMobUnit(modDataMobId, modDataMobLevel, pos.startPoint);
            if (!pos.endPoint.Equals(pos.startPoint))
            {
                entity.ReplaceMovingToPoint(new SimpleVector2Adapter(pos.endPoint));
            }

            entity.AddMobDeathReward(mobReward.Expa, mobReward.Gold, mobReward.Gems);
        }
    }
}