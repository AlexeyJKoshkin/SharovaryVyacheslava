using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class RicochetSkillSettings: BaseLevelSkillSettings
    {
        public float Radius;
        public int EnemyAmount;
        public float PhysicalDamage;
        public RicochetSkillSettings() : base(LevelSkillType.Ricochet) { }
    }
}