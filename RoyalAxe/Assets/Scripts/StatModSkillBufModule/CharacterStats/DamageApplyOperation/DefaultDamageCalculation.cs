namespace RoyalAxe.CharacterStat
{
    public abstract class DamageCalculation : IDamageApplyOperation
    {
        private static readonly HitDamageInfo Empty = new HitDamageInfo();

        public HitDamageInfo ApplyTo(UnitsEntity target, float damage)
        {
            var modificator = CreateDamageMod(target, damage);
            if (modificator.ModValue.Equals(CharacterStatValue.Default000))
            {
                return Empty;
            }

            CalcResistance(modificator);
            modificator.ApplyMod();
            target.ReplaceComponent(UnitsComponentsLookup.Health, target.health);

            return new HitDamageInfo
            {
                HitValue   = modificator.ModValue.Value,
                DamageType = DamageType.Physical
            };
        }

        public abstract float PowerDamage(UnitsEntity attacker, float mobDamage);

        private ICharacterStatModificator CreateDamageMod(UnitsEntity target, float damage)
        {
            return target.health.ChangeValue(-damage); // наносим урон здоровью
        }

        protected abstract void CalcResistance(ICharacterStatModificator modificator);
    }
}
