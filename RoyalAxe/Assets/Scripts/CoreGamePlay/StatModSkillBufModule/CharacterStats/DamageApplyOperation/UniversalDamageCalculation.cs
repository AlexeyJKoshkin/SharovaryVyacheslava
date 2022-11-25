using Core;

namespace RoyalAxe.CharacterStat
{
    public class UniversalDamageCalculation : DamageCalculation
    {
        private readonly int _powerDamageStat;

        public UniversalDamageCalculation(int powerDamageStat)
        {
            _powerDamageStat = powerDamageStat;
        }

        public override float PowerDamage(UnitsEntity attacker, float mobDamage)
        {
            var powerDamageStat =  attacker.GetComponent(_powerDamageStat) as ModifiableStat;

            if (powerDamageStat == null)
            {
                HLogger.LogError($"Not found {UnitsComponentsLookup.componentNames[_powerDamageStat]} as stat at unit");
                return mobDamage;
            }
            return powerDamageStat.CurrentValue + mobDamage;
        }

        protected override void CalcResistance(ICharacterStatModificator modificator)
        {
        }
    }
}
