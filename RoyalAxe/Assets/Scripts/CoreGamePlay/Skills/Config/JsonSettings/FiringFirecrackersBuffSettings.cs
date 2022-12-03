using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class FiringFirecrackersSkillSettings : BaseLevelSkillSettings
    {
        [ColumnName("Quantity_shells")] public int Amount;
        [ColumnName("Time_explosion")] public float Delay;
        [ColumnName("Radius_damage")] public float Radius;
        [ColumnName("Cooldown_attack")] public float Cooldown;
        [ColumnName("Physic_damage_min")] public int Physic_damage_min;
        [ColumnName("Physic_damage_max")] public int Physic_damage_max;
        public FiringFirecrackersSkillSettings() : base(LevelSkillType.FiringFirecrackers) { }
    }
}