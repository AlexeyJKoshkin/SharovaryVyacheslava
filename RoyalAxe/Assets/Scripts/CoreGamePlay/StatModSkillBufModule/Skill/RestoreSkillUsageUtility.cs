namespace RoyalAxe.Units.Stats
{
    /// <summary>
    ///     Штука которая восстанавливает скиллUsage
    /// </summary>
    public class RestoreSkillUsageUtility : IDoneTimerListener
    {
        private readonly SkillEntity _skill;

        public RestoreSkillUsageUtility(SkillEntity skill)
        {
            _skill = skill;
        }

        public void OnDoneTimer(GameRootLoopEntity timer)
        {
            var old = _skill.useCounterSkill;
            _skill.ReplaceUseCounterSkill(old.CurrentValue + _skill.restoreAttemptsTimer.RestoreAmount, old.MaxValue);
            timer.isActiveTimer = _skill.useCounterSkill.NeedRestore;
        }
    }
}