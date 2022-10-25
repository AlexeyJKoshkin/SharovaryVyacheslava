using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public class SpawnMobNode : SequenceNode
    {
        private readonly CoreGamePlayEntity _coreGameState;
        private readonly IMobAtLevelDirector _mobAtLevelDirector;

        public SpawnMobNode(CoreGamePlayContext coreGameState, IMobAtLevelDirector mobAtLevelDirector) : base("Начинаем спавн мобов")
        {
            _coreGameState      = coreGameState.levelWaveEntity;
            _mobAtLevelDirector = mobAtLevelDirector;

            new BehaviourTreeBuilder().Sequence(this)
                                      .Condition("Можно ли начать спавн,", CheckCanSpawn)
                                      .Do("Спавн", DoSpawn).End().Build();
        }

        private bool CheckCanSpawn(TimeData arg)
        {
            return _coreGameState.isWaveMobReady;
        }

        private BehaviourTreeStatus DoSpawn(TimeData arg)
        {
            _mobAtLevelDirector.StartWaveImmediate();
            _coreGameState.isWaveMobReady = false;
            return BehaviourTreeStatus.Success;
        }
    }
}