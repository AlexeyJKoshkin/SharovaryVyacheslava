using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamageBuffSettings : BaseLevelBuffSettings
    {
        public abstract DamageType DamageTypeType { get; }
        public float PercentActiveDamage;
        public float Cooldown;
        protected AdditionalDamageBuffSettings(LevelBuffType type) : base(type) { }
    }

    public class FireAdditionalDamageBuffSettings : AdditionalDamageBuffSettings
    {
        public override DamageType DamageTypeType { get; } = DamageType.Fire;

        public FireAdditionalDamageBuffSettings() : base(LevelBuffType.FireAdditionDamage)
        {
        }
    }

    public class PoisonAdditionalDamageBuffSettings : AdditionalDamageBuffSettings
    {
        public override DamageType DamageTypeType { get; } = DamageType.Poison;
        public PoisonAdditionalDamageBuffSettings() : base(LevelBuffType.PoisonAdditionDamage) { }
    }
    
    public class ColdAdditionalDamageBuffSettings : AdditionalDamageBuffSettings
    {
        public override DamageType DamageTypeType { get; } = DamageType.Cold;
        public ColdAdditionalDamageBuffSettings() : base(LevelBuffType.ColdAdditionDamage) { }
    }
}