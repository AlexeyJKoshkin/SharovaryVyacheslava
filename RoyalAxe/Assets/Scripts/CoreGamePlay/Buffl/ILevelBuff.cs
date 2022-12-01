using Core.Data.Provider;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public interface ILevelPowerStrategy
    {
        LevelBuffType Type { get; }
        bool IsSingle { get; }
        bool IsActive { get; }
        void Activate();
        void DeActivate();
    }
}