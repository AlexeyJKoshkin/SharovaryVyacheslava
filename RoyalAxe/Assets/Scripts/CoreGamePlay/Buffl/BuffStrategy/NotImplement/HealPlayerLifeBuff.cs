
namespace RoyalAxe.LevelBuff
{
    public class HealPlayerLifeBuff : AbstractBuffStrategy<HealPlayerLifeBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        
        private readonly UnitsContext _unitsContext;
        

        public HealPlayerLifeBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoBuffStrategyActivate()
        {
            Player.health.ChangeValue().FromActualMax(Settings.HealPercent).ApplyPermanentMod();
            Player.ReplaceComponent(UnitsComponentsLookup.Health, Player.health);
        }
    }
}