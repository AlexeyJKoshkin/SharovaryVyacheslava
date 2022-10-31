using Core.Data.Provider;

namespace RoyalAxe.LevelBuff
{
    public interface ILevelBuff
    {
        LevelBuffType Type { get; }
        bool IsSingle { get; }
        void Activate();
    }
}