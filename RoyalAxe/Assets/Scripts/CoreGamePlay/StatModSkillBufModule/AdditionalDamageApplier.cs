namespace RoyalAxe.Units.Stats {
    public class AdditionalDamageApplier : IInfluenceApplier
    {
        private readonly IUnitsInfluenceCalculator _calculator;
        private readonly SkillConfigDef.Damage _damage;
            
        public AdditionalDamageApplier(IUnitsInfluenceCalculator calculator, SkillConfigDef.Damage damage)
        {
            _calculator = calculator;
            _damage     = damage;
        }

        void IInfluenceApplier.Apply(UnitsEntity attacker, UnitsEntity target)
        {
            float mainPhysDamage = attacker.mainDamage.Influence.GetSingleValue(DamageType.Physical);

            _calculator.ApplySingleDamage(attacker, target, new SingleDamageInfo()
            {
                DamageType = _damage.ElementalDamageType,
                Value      = mainPhysDamage * _damage.ElementalDamage
            });
        }
    }
}