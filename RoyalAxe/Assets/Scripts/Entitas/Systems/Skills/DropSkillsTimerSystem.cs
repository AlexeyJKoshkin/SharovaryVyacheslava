using Entitas;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.EntitasSystems
{
    /// <summary>
    ///     уничтожаем таймы с удаленного компонента
    /// </summary>
    public class DropSkillsTimerSystem : IInitializeSystem, ITearDownSystem
    {
        private IGroup<SkillEntity> _restoreTimers;

        public DropSkillsTimerSystem(SkillContext skillContext)
        {
            _restoreTimers                 =  skillContext.GetGroup(Matcher<SkillEntity>.AnyOf(SkillMatcher.RestoreAttemptsTimer));
            _restoreTimers.OnEntityRemoved += RestoreTimersOnOnEntityRemoved;
        }

        private void RestoreTimersOnOnEntityRemoved(IGroup<SkillEntity> group, SkillEntity entity, int index, IComponent component)
        {
            if (component is BaseTimerCounterComponent timer)
            {
                timer.Timer.Destroy();
            }
        }

        public void Initialize()
        {
            _restoreTimers.OnEntityRemoved += RestoreTimersOnOnEntityRemoved;
        }

        public void TearDown()
        {
            _restoreTimers.OnEntityRemoved -= RestoreTimersOnOnEntityRemoved;
        }
    }
}