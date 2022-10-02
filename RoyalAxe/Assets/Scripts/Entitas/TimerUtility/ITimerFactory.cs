using Entitas;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.EntitasSystems.TimerUtility
{
    public interface ITimerFactory
    {
        IRATimer CreateTimer(float time, bool isRepeat = false);

        IRATimer CreateRestoreUsageSkillTimer(SkillEntity skill, float rangeParamsRangeCooldownAttack);
    }

    public class RATimerFactory : ITimerFactory
    {
        private IContext<GameRootLoopEntity> _mainContext;

        public RATimerFactory(IContext<GameRootLoopEntity> mainContext)
        {
            _mainContext = mainContext;
        }

        public IRATimer CreateTimer(float time, bool isRepeat = false)
        {
            var entity = _mainContext.CreateEntity();
            entity.isRepeat = isRepeat;
            entity.AddTimer(0, time);
            return new RASimpleTimerWrapper(entity);
        }

        public IRATimer CreateRestoreUsageSkillTimer(SkillEntity skill, float cooldown)
        {
            var timer = CreateTimer(cooldown, true);
            timer.AddDoneHandler(new RestoreSkillUsageUtility(skill));
            return timer;
        }
    }
}