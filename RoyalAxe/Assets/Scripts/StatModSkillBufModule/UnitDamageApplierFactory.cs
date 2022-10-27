namespace RoyalAxe.CharacterStat
{
    public interface IUnitDamageApplierFactory
    {
        ISimpleDamageApplier CreateOneMomentDamage(DamageType type, float damage);
        IPeriodicDamageApplier CreatePeriodicDamage(PeriodicDamageInfluenceData periodicDamageInfluenceData);
        IEntityBuff CreateElementalDamage(UnitsEntity attacker, PeriodicDamageInfluenceData damage);
    }
    
    public class UnitDamageApplierFactory : IUnitDamageApplierFactory
    {
        private readonly IUnitsInfluenceCalculator _calculator;
        public UnitDamageApplierFactory(IUnitsInfluenceCalculator calculator)
        {
            _calculator = calculator;
        }

        public IEntityBuff CreateElementalDamage(UnitsEntity attacker, PeriodicDamageInfluenceData damage)
        {
            var result =  new ElementalDamageBuf(_calculator, attacker, damage);
            return result;
        }

        public ISimpleDamageApplier CreateOneMomentDamage(DamageType type, float damage)
        {
            var damageData = new DamageInfluenceData(damage, type);
            return new OneMomentDamageOperation(damageData,_calculator);
            
        }

        public IPeriodicDamageApplier CreatePeriodicDamage(PeriodicDamageInfluenceData periodicDamageInfluenceData)
        {
            return new ElementalDamageBuf.ElementalBufApplyHelper(periodicDamageInfluenceData, this);
        }
    }
}
