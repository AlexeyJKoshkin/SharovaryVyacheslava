using RoyalAxe.Units.Stats;

namespace RoyalAxe
{

    public interface IInfluenceApplier
    {
        void Apply(UnitsEntity attacker, UnitsEntity target);
    }


    public interface IPeriodicInfluenceApplier : IInfluenceApplier
    {
        SkillConfigDef.Damage DamageData { get; }
    }
}