namespace RoyalAxe.LevelBuff
{
    public class SequentialWeaponRollPower : AbstractPowerStrategyStrategy
    {
        private readonly UnitsContext _unitsContext;

        public SequentialWeaponRollPower(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public override LevelBuffType Type => LevelBuffType.SequentialWeaponRoll;

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