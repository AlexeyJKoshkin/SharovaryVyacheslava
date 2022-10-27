using RoyalAxe.CharacterStat;

namespace RoyalAxe
{
    public interface IDamageApplier
    {
        void Apply(UnitsEntity attacker, UnitsEntity target);
        DamageType Type { get; }
    }

    public interface ISimpleDamageApplier : IDamageApplier
    {
        float Value { get; }
        void AddDamage(float damageValue);
    }

    public interface IPeriodicDamageApplier : IDamageApplier
    {
        
    }
}