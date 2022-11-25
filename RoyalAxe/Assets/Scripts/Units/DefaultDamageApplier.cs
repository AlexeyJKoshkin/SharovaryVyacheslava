using RoyalAxe.CharacterStat;

namespace RoyalAxe
{
    public interface IInfluenceApplier
    {
        void Apply(UnitsEntity attacker, UnitsEntity target);
     
    }

    public interface ISimpleInfluenceApplier : IInfluenceApplier
    {
        DamageType Type { get; }
        float Value { get; }
        void AddDamage(float damageValue);
    }

    public interface IPeriodicInfluenceApplier : IInfluenceApplier
    {
    }

    public interface IDeBuffApplier : IInfluenceApplier
    {
        
    }
}