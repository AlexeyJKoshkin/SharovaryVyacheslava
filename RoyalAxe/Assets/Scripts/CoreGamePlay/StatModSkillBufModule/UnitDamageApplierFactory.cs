namespace RoyalAxe.CharacterStat
{
    public class UnitDamageApplierFactory : IUnitDamageApplierFactory
    {
        private readonly IUnitsInfluenceCalculator _calculator;

        public UnitDamageApplierFactory(IUnitsInfluenceCalculator calculator)
        {
            _calculator = calculator;
        }

        public IEntityBuff CreateElementalDamageBuf(UnitsEntity attacker, SkillConfigDef.Damage damage)
        {
            var result = new ElementalDamageBuf(_calculator, attacker, damage);
            return result;
        }

        public InfluenceApplierComposite CreateComposite()
        {
            return new InfluenceApplierComposite(_calculator);
        }


        public IInfluenceApplier CreateAdditionalDamageApplier(SkillConfigDef.Damage settingsDamage)
        {
            return new AdditionalDamageApplier(_calculator, settingsDamage);
        }


        public IPeriodicInfluenceApplier CreatePeriodicDamage(SkillConfigDef.Damage periodicDamageInfluenceData)
        {
            return new ElementalDamageBuf.ElementalBufApplyHelper(periodicDamageInfluenceData, this);
        }
    }

    //может применятся и сниматься в разных местах и по нескольку раз.
}