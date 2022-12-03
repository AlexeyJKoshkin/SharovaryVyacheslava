using System;
using Core.Parser;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public abstract class AdditionalDamageSkillSettings : BaseLevelSkillSettings
    {
        public abstract DamageType DamageTypeType { get; }
        public SkillConfigDef.Damage Damage;
        protected AdditionalDamageSkillSettings(LevelSkillType type) : base(type) { }
    }
    
    [Serializable]
    public class FireAdditionalDamageSkillSettings : AdditionalDamageSkillSettings
    {
        public override DamageType DamageTypeType { get; } = DamageType.Fire;

        public FireAdditionalDamageSkillSettings() : base(LevelSkillType.FireAdditionDamage) { }
    }

    [Serializable]
    public class PoisonAdditionalDamageSkillSettings : AdditionalDamageSkillSettings
    {
        public override DamageType DamageTypeType { get; } = DamageType.Poison;
        public PoisonAdditionalDamageSkillSettings() : base(LevelSkillType.PoisonAdditionDamage) { }
    }

    [Serializable]
    public class ColdAdditionalDamageSkillSettings : AdditionalDamageSkillSettings
    {
        [ColumnName("Deceleration")] public float DecelerationPercent;
        public override DamageType DamageTypeType { get; } = DamageType.Cold;
        public ColdAdditionalDamageSkillSettings() : base(LevelSkillType.ColdAdditionDamage) { }
    }
}
