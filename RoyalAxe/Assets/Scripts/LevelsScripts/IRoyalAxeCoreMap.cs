
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel
{
    public interface IEnemyWaveGenerator 
    { 
        void GenerateEnemy(MobBlueprint mobBlueprint);
        int CurrentMobAmount { get; }
    }

    public interface IRoyalAxeCoreMap
    {
        IEnemyWaveGenerator StartGenerateMobPosition();
        int CurrentMobAmount { get; }
    }


}
