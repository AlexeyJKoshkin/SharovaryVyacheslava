
namespace RoyalAxe.CoreLevel
{
    public interface IEnemyWaveGenerator { 
        void GenerateEnemy(string modDataMobId, byte modDataMobLevel);
        int CurrentMobAmount { get; }
    }

    public interface IRoyalAxeCoreMap
    {
        IEnemyWaveGenerator StartGenerateMobPosition();
    }


}
