namespace RoyalAxe.CharacterStat
{
    public interface IUnitDamageApplierFactory
    {
        IPeriodicInfluenceApplier CreatePeriodicDamage(SkillConfigDef.Damage periodicDamageInfluenceData);
        IEntityBuff CreateElementalDamage(UnitsEntity attacker, SkillConfigDef.Damage damage);

        IInfluenceApplierComposite CreateComposite(params SkillConfigDef.Damage[] damageData);
    }
    
    public class UnitDamageApplierFactory : IUnitDamageApplierFactory
    {
        private readonly IUnitsInfluenceCalculator _calculator;
        public UnitDamageApplierFactory(IUnitsInfluenceCalculator calculator)
        {
            _calculator = calculator;
        }

        public IEntityBuff CreateElementalDamage(UnitsEntity attacker, SkillConfigDef.Damage damage)
        {
            var result =  new ElementalDamageBuf(_calculator, attacker, damage);
            return result;
        }

        public IInfluenceApplierComposite CreateComposite(params SkillConfigDef.Damage[] damageData)
        {
            if(damageData == null || damageData.Length == 0) return null;
            
            var composite = new InfluenceApplierComposite(this,_calculator);
            for (int i = 0; i < damageData.Length; i++)
            {
               composite.Upgrade(damageData[i]);
            }
            return composite;
        }


        public IPeriodicInfluenceApplier CreatePeriodicDamage(SkillConfigDef.Damage periodicDamageInfluenceData)
        {
            return new ElementalDamageBuf.ElementalBufApplyHelper(periodicDamageInfluenceData, this);
        }
    }
}
