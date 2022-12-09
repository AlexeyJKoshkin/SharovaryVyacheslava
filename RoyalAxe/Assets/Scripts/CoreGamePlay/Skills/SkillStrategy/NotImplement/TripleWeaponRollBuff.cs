namespace RoyalAxe.LevelSkill
{
    public class TripleWeaponRollPlayerSkill : AbstractPlayerSkillStrategy
    {
        private readonly UnitsContext _unitsContext;
        public override LevelSkillType Type => LevelSkillType.TripleWeapon;
        public override bool IsSingle => true;

        public TripleWeaponRollPlayerSkill(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate()
        {
            _unitsContext.playerEntity.unitActiveSkill.SkillEntity.isTripleAxe = true;
        }

        public override void DoLevelPowerDeActivate()
        {
            _unitsContext.playerEntity.unitActiveSkill.SkillEntity.isTripleAxe = false;
        }
    }
}