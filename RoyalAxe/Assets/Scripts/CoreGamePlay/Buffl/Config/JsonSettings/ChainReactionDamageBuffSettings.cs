using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class ChainReactionDamageBuffSettings : BaseLevelBuffSettings
    {
        public int EnemyAmount;
        public int Damage;
        public float DamagePercentReduction;
        public ChainReactionDamageBuffSettings() : base(LevelBuffType.ChainReactionDamage) { }
    }
    
   
}