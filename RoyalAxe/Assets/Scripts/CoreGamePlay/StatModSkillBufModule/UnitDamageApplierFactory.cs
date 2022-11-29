namespace RoyalAxe.CharacterStat
{
    public interface IUnitDamageApplierFactory
    {
        IPeriodicInfluenceApplier CreatePeriodicDamage(PeriodicDamageInfluenceData periodicDamageInfluenceData);
        IEntityBuff CreateElementalDamage(UnitsEntity attacker, PeriodicDamageInfluenceData damage);

        IInfluenceApplierComposite CreateComposite(params SkillConfigDef.Damage[] damageData);
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

        public IInfluenceApplierComposite CreateComposite(params SkillConfigDef.Damage[] damageData)
        {
            if(damageData == null || damageData.Length == 0) return null;
            
            var composite = new InfluenceApplierComposite(this,_calculator);
            for (int i = 0; i < damageData.Length; i++)
            {
                Add(damageData[i]);
            }

            void Add(SkillConfigDef.Damage data)
            {
                composite.IncreaseDamage(DamageType.Physical, data.PhysicalDamage);
            

                if (data.ElementalDamage > 0 && data.DamageCooldown <= 0) // есть одномоментный магический урон
                {
                    composite.IncreaseDamage(data.ElementalDamageType,data.ElementalDamage);
                }

                if (data.ElementalDamage > 0 && data.DamageCooldown > 0) // есть елементальный урон размазанный по времени
                {
                    var periodicDamageInfluenceData = new PeriodicDamageInfluenceData
                    {
                        MagicDuration       = data.MagicDuration,
                        DamageCooldown      = data.DamageCooldown,
                        Damage              = data.ElementalDamage,
                        ElementalDamageType = data.ElementalDamageType
                    };

                    composite.AddPeriodicDamage(periodicDamageInfluenceData);
                }
            }

            return composite;
        }


        public IPeriodicInfluenceApplier CreatePeriodicDamage(PeriodicDamageInfluenceData periodicDamageInfluenceData)
        {
            return new ElementalDamageBuf.ElementalBufApplyHelper(periodicDamageInfluenceData, this);
        }
    }
}
