namespace RoyalAxe.LevelBuff
{
    public interface ILevelBuff
    {
        bool IsSingle { get; }
        void Activate();
    }
}