namespace RoyalAxe.CharacterStat
{
    internal class GetMaxValue : IStatValueProvider
    {
        public float GetValue(CharacterStatValue stat)
        {
            return stat.MaxValue;
        }

        public CharacterStatValue SetValue(float newValue)
        {
            return new CharacterStatValue
            {
                MaxValue = newValue
            };
        }
    }
}