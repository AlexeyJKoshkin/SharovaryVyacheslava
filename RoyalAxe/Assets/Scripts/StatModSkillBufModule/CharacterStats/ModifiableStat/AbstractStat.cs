namespace RoyalAxe.CharacterStat
{
    public abstract class AbstractStat : IGameStat
    {
        /*public abstract CharacterStatValue ActualStatValue { get;  }
        public abstract CharacterStatValue NativeStatValue { get; }
        public abstract CharacterStatValue ModStatValue();*/
        public abstract float CurrentValue { get; }
        public abstract float MaxValue { get; }
        public abstract float MinValue { get; }
        public abstract void Reset();
    }
}