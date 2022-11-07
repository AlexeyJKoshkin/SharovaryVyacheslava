using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public interface ILevelGamePlayNodeBehavior : IBehaviourTreeNode
    {
        void StartLevel();
        void EndLevel();
    }

    public class CoreGameBehaviourNode : ParallelNode
    {
        public CoreGameBehaviourNode(ShowWinWindowNode showWinWindowNode,
                                     SpawnMobNode spawnMobNode,
                                     LoadNextWaveNode loadNextWave) : base("Выполняем проверки паралельно", -1, -1)
        {
            new BehaviourTreeBuilder()
                .Parent(this)
                    .Do(showWinWindowNode) // пробуем закончить игру
                    .Do(spawnMobNode)      // пробуем заспавнить мобов
                    .Do(loadNextWave)      // пробуем загрузить следующую волну
                .End()
            .Build();
        }
    }
}