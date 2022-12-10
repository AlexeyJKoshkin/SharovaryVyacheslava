namespace RoyalAxe.Units.Stats
{
    public abstract class AbstractStat : IGameStat
    {
        public abstract float CurrentValue { get; }
        public abstract float MaxValue { get; }
        public abstract float MinValue { get; }
        public abstract void Reset();
    }
}