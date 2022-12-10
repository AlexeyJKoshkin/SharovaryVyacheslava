using RoyalAxe.Units.Stats;

namespace RoyalAxe.Units.Stats
{
    public interface ICharacterStatModificator : IModApplier
    {
        CharacterStatValue ModValue { get; }

        bool RemoveMode();

        IGameStat Stat { get; }
    }
}