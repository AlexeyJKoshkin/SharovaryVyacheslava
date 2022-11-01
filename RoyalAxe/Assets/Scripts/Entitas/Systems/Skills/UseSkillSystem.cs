using Entitas;

namespace RoyalAxe.EntitasSystems
{
    /// <summary>
    /// Умеет запускать скилы для разных персонажей
    /// </summary>
    public abstract class UpdateUsagesSystem : RAReactiveSystem<SkillEntity>
    {
        protected UpdateUsagesSystem(IContext<SkillEntity> context) : base(context) { }

        protected override bool Filter(SkillEntity entity)
        {
            return entity.useCounterSkill.CanUse;
        }

        protected override void Execute(SkillEntity skill)
        {
            DoSkillAction(skill); // запускаем скилл

            var currentCounter = skill.useCounterSkill; // заменили количество зарядов
            skill.ReplaceUseCounterSkill(currentCounter.CurrentValue - skill.priceUseSkill.Price, currentCounter.MaxValue);
            if (skill.hasRestoreAttemptsTimer)
            {
                skill.restoreAttemptsTimer.Run(); // запустили таймер
            }
        }

        protected abstract void DoSkillAction(SkillEntity skill);
    }
}