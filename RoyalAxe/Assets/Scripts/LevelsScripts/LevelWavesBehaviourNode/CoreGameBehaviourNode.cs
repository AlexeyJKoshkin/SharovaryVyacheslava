using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public class CoreGameBehaviourNode : ParallelNode
    {
        public CoreGameBehaviourNode(ShowWinWindowNode showWinWindowNode,
                                     SpawnMobNode spawnMobNode,
                                     LoadNextWaveNode loadNextWave) : base("Выполняем проверки паралельно", -1, -1)
        {
            new BehaviourTreeBuilder()
                .Parent(this)
                .Do(showWinWindowNode)
                .Do(spawnMobNode)
                .Do(loadNextWave)
                .End()
                .Build();
        }
    }
}