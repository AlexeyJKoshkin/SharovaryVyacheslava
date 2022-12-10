using RoyalAxe.GameEntitas;

namespace RoyalAxe.Units.Stats
{
    public class UnitDamageApplierFactory : IUnitDamageApplierFactory
    {
        private readonly IUnitsInfluenceCalculator _calculator;
        private readonly IBuffFactory _buffFactory;

        public UnitDamageApplierFactory(IUnitsInfluenceCalculator calculator, IBuffFactory buffFactory)
        {
            _calculator = calculator;
            _buffFactory = buffFactory;
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
            return new ElementalBufApplyHelper(periodicDamageInfluenceData, _buffFactory);
        }
    }
    
}