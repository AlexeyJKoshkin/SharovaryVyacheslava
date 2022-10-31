namespace RoyalAxe.LevelBuff
{
    public class TripleWeaponRollBuff : AbstractBuffStrategy
    {
        private readonly UnitsContext _unitsContext;
        public override LevelBuffType Type => LevelBuffType.TripleWeapon;
        public override bool IsSingle => true;

        public TripleWeaponRollBuff(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public override void DoBuffStrategyActivate()
        {
            _unitsContext.playerEntity.unitActiveSkill.SkillEntity.isTripleAxe = true;
        }
    }
}