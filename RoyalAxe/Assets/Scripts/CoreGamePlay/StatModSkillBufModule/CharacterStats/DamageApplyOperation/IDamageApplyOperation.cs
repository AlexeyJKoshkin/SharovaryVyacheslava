namespace RoyalAxe.Units.Stats
{
    public interface IDamageApplyOperation
    {
        //Усилить урон
        
        float ApplyDamage(UnitsEntity target, float damage);
        float ApplyDamage(UnitsEntity attacker, UnitsEntity target, float damage);
    }
}