
namespace RoyalAxe.LevelBuff
{
    public class HealPlayerLifePower : AbstractPowerStrategyStrategy<HealPlayerLifeBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        
        private readonly UnitsContext _unitsContext;
        

        public HealPlayerLifePower(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate()
        {
            Player.health.ChangeValue().FromActualMax(Settings.HealPercent).ApplyPermanentMod();
            Player.ReplaceComponent(UnitsComponentsLookup.Health, Player.health);
        }

        public override void DoLevelPowerDeActivate()
        {
        }
    }
}