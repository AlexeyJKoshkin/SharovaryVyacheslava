namespace RoyalAxe.LevelSkill
{
    public class IncreasePlayerSkillSpeedPlayerSkill : AbstractPlayerSkillStrategy<IncreasePlayerSkillSpeedSkillSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerSkillSpeedPlayerSkill(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate()
        {
        }

        public override void DoLevelPowerDeActivate()
        {
            
        }
    }
}