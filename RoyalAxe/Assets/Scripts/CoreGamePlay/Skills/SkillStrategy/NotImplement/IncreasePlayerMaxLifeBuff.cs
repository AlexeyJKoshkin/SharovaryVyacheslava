namespace RoyalAxe.LevelSkill
{
    public class IncreasePlayerMaxLifePlayerSkill : AbstractPlayerSkillStrategy<IncreasePlayerMaxLifeSkillSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerMaxLifePlayerSkill(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate()
        {
            Player.health.ChangeMaxValue(Settings.IncreaseValue).ApplyPermanentMod();
        }

        public override void DoLevelPowerDeActivate()
        {
        }
    }
}