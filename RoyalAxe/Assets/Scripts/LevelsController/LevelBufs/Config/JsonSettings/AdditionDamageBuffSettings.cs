using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionDamageBuffSettings
    {
        public abstract DamageType Type { get; }
        public float PercentActiveDamage;
        public float Cooldown;
    }

    public class FireAdditionDamageBuffSettings : AdditionDamageBuffSettings
    {
        public override DamageType Type { get; } = DamageType.Fire;
    }

    public class PoisonAdditionDamageBuffSettings : AdditionDamageBuffSettings
    {
        public override DamageType Type { get; } = DamageType.Poison;
    }
    
    public class ColdAdditionDamageBuffSettings : AdditionDamageBuffSettings
    {
        public override DamageType Type { get; } = DamageType.Cold;
    }

    public class ChainReactionDamageBuffSettings
    {
        public int EnemyAmount;
        public int Damage;
        public float DamagePercentReduction;
    }
}