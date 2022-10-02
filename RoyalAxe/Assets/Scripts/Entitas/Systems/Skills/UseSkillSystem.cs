using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public abstract class UpdateUsagesSystem : RAReactiveSystem<SkillEntity>
    {
        protected UpdateUsagesSystem(IContext<SkillEntity> context) : base(context) { }

        protected override bool Filter(SkillEntity entity)
        {
            return entity.useCounterSkill.CanUse;
        }

        protected override void Execute(SkillEntity skill)
        {
            DoSkillAction(skill);

            var currentCounter = skill.useCounterSkill;
            skill.ReplaceUseCounterSkill(currentCounter.CurrentValue - skill.priceUseSkill.Price, currentCounter.MaxValue);
            if (skill.hasRestoreAttemptsTimer)
            {
                skill.restoreAttemptsTimer.Run();
            }
        }

        protected abstract void DoSkillAction(SkillEntity skill);
    }
}