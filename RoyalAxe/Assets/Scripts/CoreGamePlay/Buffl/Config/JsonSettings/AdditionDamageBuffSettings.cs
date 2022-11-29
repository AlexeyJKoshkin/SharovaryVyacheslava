using System;
using Core.Parser;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public abstract class AdditionalDamageBuffSettings : BaseLevelBuffSettings
    {
        public abstract DamageType DamageTypeType { get; }
        public SkillConfigDef.Damage Damage;
        protected AdditionalDamageBuffSettings(LevelBuffType type) : base(type) { }
    }
    
    [Serializable]
    public class FireAdditionalDamageBuffSettings : AdditionalDamageBuffSettings
    {
        public override DamageType DamageTypeType { get; } = DamageType.Fire;

        public FireAdditionalDamageBuffSettings() : base(LevelBuffType.FireAdditionDamage) { }
    }

    [Serializable]
    public class PoisonAdditionalDamageBuffSettings : AdditionalDamageBuffSettings
    {
        public override DamageType DamageTypeType { get; } = DamageType.Poison;
        public PoisonAdditionalDamageBuffSettings() : base(LevelBuffType.PoisonAdditionDamage) { }
    }

    [Serializable]
    public class ColdAdditionalDamageBuffSettings : AdditionalDamageBuffSettings
    {
        [ColumnName("Deceleration")] public float DecelerationPercent;
        public override DamageType DamageTypeType { get; } = DamageType.Cold;
        public ColdAdditionalDamageBuffSettings() : base(LevelBuffType.ColdAdditionDamage) { }
    }
}
