
namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamageBuff<T> : AbstractBuffStrategy<T> where T : AdditionalDamageBuffSettings
    {
        protected UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;

        protected AdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider)
        {
            _unitsContext             = unitsContext;
        }
        

        public override void DoBuffStrategyActivate()
        {
            Player.mainDamage.IncreaseDamage(Settings.DamageTypeType, Settings.Damage.ElementalDamage);
        }
       
    }
}