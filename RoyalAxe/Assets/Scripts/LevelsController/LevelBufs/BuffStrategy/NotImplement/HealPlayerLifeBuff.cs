
namespace RoyalAxe.LevelBuff
{
    public class HealPlayerLifeBuff : AbstractBuffStrategy
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        
        private readonly UnitsContext _unitsContext;
        private readonly HealPlayerLifeBuffSettings _settings;
        

        public HealPlayerLifeBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider)
        {
            _unitsContext = unitsContext;
            _settings = provider.SettingsComposite.HealPlayerLifeBuffSetting;
        }

        public override void Activate()
        {
            Player.health.ChangeValue().FromActualMax(_settings.HealPercent).ApplyPermanentMod();
            Player.ReplaceComponent(UnitsComponentsLookup.Health, Player.health);
        }
    }
}