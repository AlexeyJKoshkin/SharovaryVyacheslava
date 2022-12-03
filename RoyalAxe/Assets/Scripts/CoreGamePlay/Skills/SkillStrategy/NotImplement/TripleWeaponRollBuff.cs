namespace RoyalAxe.LevelBuff
{
    public class TripleWeaponRollPower : AbstractPowerStrategyStrategy
    {
        private readonly UnitsContext _unitsContext;
        public override LevelSkillType Type => LevelSkillType.TripleWeapon;
        public override bool IsSingle => true;

        public TripleWeaponRollPower(UnitsContext unitsContext)
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