namespace RoyalAxe.LevelSkill
{
    public class SequentialWeaponRollPlayerSkill : AbstractPlayerSkillStrategy
    {
        private readonly UnitsContext _unitsContext;

        public SequentialWeaponRollPlayerSkill(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public override LevelSkillType Type => LevelSkillType.SequentialWeaponRoll;

        public override void DoLevelPowerActivate()
        {
            _unitsContext.playerEntity.unitActiveSkill.SkillEntity.isDoubleAxe = true;
        }

        public override void DoLevelPowerDeActivate()
        {
            _unitsContext.playerEntity.unitActiveSkill.SkillEntity.isDoubleAxe = false;
        }
    }
}