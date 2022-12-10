using Core;

namespace RoyalAxe.Units.Stats
{
    public class PowerDamageOperation
    {
        private readonly int _powerDamageStat;

        public PowerDamageOperation(int powerDamageStat)
        {
            _powerDamageStat = powerDamageStat;
        }

        public float PowerDamage(UnitsEntity attacker, float damage)
        {
            var powerDamageStat = attacker.GetComponent(_powerDamageStat) as ModifiableStat;

            if (powerDamageStat == null)
            {
                HLogger.LogError($"Not found {UnitsComponentsLookup.componentNames[_powerDamageStat]} as stat at unit");
                return damage;
            }

            return powerDamageStat.CurrentValue + damage;
        }
    }

    public class UniversalDamageCalculation : IDamageApplyOperation
    {
        public PowerDamageOperation PowerDamageOperation;
        public ResistanceOperation Resistance;
    


        public float ApplyDamage(UnitsEntity target, float damage)
        {
            var modificator = CreateDamageMod(target, damage);
            if (modificator.ModValue.Equals(CharacterStatValue.Default000))
            {
                return 0;
            }


            modificator.ApplyMod();
            return modificator.ModValue.Value;
        }

        public float ApplyDamage(UnitsEntity attacker, UnitsEntity target, float damage)
        {
            if (PowerDamageOperation != null)
                damage = PowerDamageOperation.PowerDamage(attacker, damage);
            return ApplyDamage(target, damage);
        }


        public ICharacterStatModificator CreateDamageMod(UnitsEntity target, float damage)
        {
            var mod = target.health.ChangeValue(-damage); // наносим урон здоровью 
            return new UnitCharacterStatModificatorDecorator(mod, UnitsComponentsLookup.Health, target);
        }
    }
}