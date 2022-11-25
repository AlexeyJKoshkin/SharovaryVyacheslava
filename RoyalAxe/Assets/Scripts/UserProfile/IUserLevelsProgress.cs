namespace Core.UserProfile
{
    public interface IUserLevelsProgress : IUserProgressProfile
    {
        LastLevel SavedLevel { get; set; }

        LastLevel LastPlayed { get; set; }
    }
}