
namespace RoyalAxe.LevelSkill
{
    public class HealPlayerLifePlayerSkill : AbstractPlayerSkillStrategy<HealPlayerLifeSkillSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        
        private readonly UnitsContext _unitsContext;
        

        public HealPlayerLifePlayerSkill(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
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