namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerSkillUsageBuff : AbstractBuffStrategy
    {
        private int Increase_Amount = 1;
        
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerSkillUsageBuff(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public override void Activate()
        {
            UnitsEntity player      = null;
            var         skillEntity = player.unitActiveSkill.SkillEntity;
            var         usages      = skillEntity.useCounterSkill;
            skillEntity.ReplaceUseCounterSkill(usages.CurrentValue + Increase_Amount, usages.MaxValue + Increase_Amount);
        }
    }
}