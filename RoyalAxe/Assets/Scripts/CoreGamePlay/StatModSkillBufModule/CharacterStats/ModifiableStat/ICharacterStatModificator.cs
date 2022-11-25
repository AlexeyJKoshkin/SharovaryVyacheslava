namespace RoyalAxe.CharacterStat
{
    public interface ICharacterStatModificator : IModApplier
    {
        CharacterStatValue ModValue { get; }

        bool RemoveMode();

        IGameStat Stat { get; }
    }
}