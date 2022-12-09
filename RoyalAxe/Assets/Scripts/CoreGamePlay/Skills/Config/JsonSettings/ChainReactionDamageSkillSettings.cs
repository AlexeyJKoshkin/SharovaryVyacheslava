using System;

namespace RoyalAxe.LevelSkill {
    [Serializable]
    public class ChainReactionDamageSkillSettings : BaseLevelSkillSettings
    {
        public int EnemyAmount;
        public int Damage;
        public float DamagePercentReduction;
        public ChainReactionDamageSkillSettings() : base(LevelSkillType.ChainReactionDamage) { }
    }
}