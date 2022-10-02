namespace RoyalAxe.CharacterStat
{
    public interface IDamageApplyOperation
    {
        //Применить урон
        HitDamageInfo ApplyTo(UnitsEntity modificator, float damage);
        //Усилить урон
        float PowerDamage(UnitsEntity attacker, float mobDamage);
    }
}