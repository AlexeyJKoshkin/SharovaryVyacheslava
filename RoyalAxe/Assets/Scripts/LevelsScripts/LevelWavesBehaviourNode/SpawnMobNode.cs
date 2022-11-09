using FluentBehaviourTree;
using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.CoreLevel
{
    //Пробуем спавнить мобов
    public class SpawnMobNode : SequenceNode
    {
        private readonly CoreGamePlayEntity _coreGameState;
        private readonly IMobSpawnFacade _mobSpawnFacade;
        
       public SpawnMobNode(CoreGamePlayContext coreGameState,
                           IMobSpawnFacade mobSpawnOperation) : base("Начинаем спавн мобов")
        {
            _coreGameState      = coreGameState.levelWaveEntity;
            _mobSpawnFacade = mobSpawnOperation;

            new BehaviourTreeBuilder().Sequence(this)
                                      .Condition("Можно ли начать спавн,", CheckCanSpawn)
                                      .Do("Спавн", DoSpawn).End().Build();
        }

        private bool CheckCanSpawn(TimeData arg)
        {
            return false;
        }

        private BehaviourTreeStatus DoSpawn(TimeData arg)
        {
            _mobSpawnFacade.StartSpawnMob();
            return BehaviourTreeStatus.Success;
        }
    }
}