namespace Core.UserProfile
{
    public class LevelProgressBuilderDefault : BaseDefaultProgressFactory<UserLevelProgress>
    {
        public override UserLevelProgress CreateDefault()
        {
            return new UserLevelProgress
            {
                LastPlayedLevel = LastLevel.Default,
                LastSavedLevel  = LastLevel.Default
            };
        }
    }
}