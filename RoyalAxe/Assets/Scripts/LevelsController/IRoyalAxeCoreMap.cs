using Entitas;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public interface IEnemyWaveGenerator { 
        void GenerateEnemy(string modDataMobId, byte modDataMobLevel, MobDeathReward mobReward);
        int CurrentMobAmount { get; }
    }

    public interface IRoyalAxeCoreMap
    {
        IEnemyWaveGenerator StartGenerateMobPosition();
    }


}
