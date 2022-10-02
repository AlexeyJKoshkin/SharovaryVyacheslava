namespace RoyalAxe.LevelBuff
{
    public class SequentialWeaponRollBuff : AbstractBuffStrategy
    {
        private readonly UnitsContext _unitsContext;
        public override bool IsSingle => true;

        public SequentialWeaponRollBuff(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public override void Activate()
        {
            _unitsContext.playerEntity.unitActiveSkill.SkillEntity.isDoubleAxe = true;
        }
    }
}