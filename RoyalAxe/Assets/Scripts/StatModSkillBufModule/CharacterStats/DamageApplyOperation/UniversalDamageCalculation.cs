using Core;

namespace RoyalAxe.CharacterStat
{
    public class UniversalDamageCalculation : DamageCalculation
    {
        private readonly int _indexComponent;

        public UniversalDamageCalculation(int indexComponent)
        {
            _indexComponent = indexComponent;
        }

        public override float PowerDamage(UnitsEntity attacker, float mobDamage)
        {
            var powerDamageStat =  attacker.GetComponent(_indexComponent) as ModifiableStat;

            if (powerDamageStat == null)
            {
                HLogger.LogError($"Not found {UnitsComponentsLookup.componentNames[_indexComponent]} as stat at unit");
                return mobDamage;
            }
            return powerDamageStat.CurrentValue + mobDamage;
        }

        protected override void CalcResistance(ICharacterStatModificator modificator)
        {
        }
    }
}
