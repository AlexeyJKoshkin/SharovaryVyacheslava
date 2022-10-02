namespace RoyalAxe.CharacterStat
{
    public interface IGameStat
    {
        float CurrentValue { get; }
        float MaxValue { get; }
        float MinValue { get; }

        /*CharacterStatValue ActualStatValue { get; }
        CharacterStatValue NativeStatValue { get; }
        CharacterStatValue ModStatValue();*/
        void Reset();
    }
}