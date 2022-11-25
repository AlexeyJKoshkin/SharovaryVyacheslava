namespace RoyalAxe.LevelBuff
{
    public class SequentialWeaponRollBuff : AbstractBuffStrategy
    {
        private readonly UnitsContext _unitsContext;

        public SequentialWeaponRollBuff(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public override LevelBuffType Type => LevelBuffType.SequentialWeaponRoll;

        public override void DoBuffStrategyActivate()
        {
            _unitsContext.playerEntity.unitActiveSkill.SkillEntity.isDoubleAxe = true;
        }
    }
}