using System.Linq;
using Core;
using Entitas;

using RoyalAxe.GameEntitas;
using VContainer.Unity;


namespace RoyalAxe.CoreLevel
{
    public class MockLevelCoreMap : IRoyalAxeCoreMap, IEnemyWaveGenerator
    {
        public int CurrentMobAmount => _allMobs.count; //Количество текущих мобов на уровне
        private readonly IMobPositionGenerator _generator;
        private readonly IGroup<UnitsEntity> _allMobs;
        private readonly IUnitsBuilderFacade _unitsBuilder;
        private readonly CoreGamePlayContext _coreGamePlay;



        public MockLevelCoreMap(UnitsContext unitsContext,
                                IUnitsBuilderFacade unitsBuilder,
                                IMobPositionGenerator mobPositionGenerator,
                                CoreGamePlayContext levelWaveProvider)
        {
            _unitsBuilder = unitsBuilder;

            _allMobs = unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.Mob, UnitsMatcher.UnitsView)
                                                                 .NoneOf(UnitsMatcher.DeadUnit, UnitsMatcher.Boson, UnitsMatcher.DestroyUnit));

            _generator = mobPositionGenerator;
            _coreGamePlay = levelWaveProvider;
        }

        IEnemyWaveGenerator IRoyalAxeCoreMap.StartGenerateMobPosition()
        {
            _generator.Reset();
            return this;
        }

        void IEnemyWaveGenerator.GenerateEnemy(MobBlueprint mobBlueprint)
        {
         //   var mobGems = _coreGamePlay.levelWaveEntity.levelWaveQueue.Current.GemsPerLevel;
            var pos = _generator.GetPosForNewMob(mobBlueprint.Id);
            mobBlueprint.Position = pos.startPoint;
            var entity = _unitsBuilder.CreateEnemyMobUnit(mobBlueprint);

            if (!pos.endPoint.Equals(pos.startPoint))
            {
                if(entity.hasNavMeshAgent)
                    entity.navMeshAgent.SetDestinationPoint(pos.endPoint);
            }

            var reward = mobBlueprint.DeathReward;
            entity.AddMobDeathReward(reward.Expa, reward.Gold, 0);
        }

      
    }
}
