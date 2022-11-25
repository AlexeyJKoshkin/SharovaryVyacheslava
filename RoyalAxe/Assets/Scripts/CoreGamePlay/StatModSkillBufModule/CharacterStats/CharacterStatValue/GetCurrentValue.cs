namespace RoyalAxe.CharacterStat
{
    internal class GetCurrentValue : IStatValueProvider
    {
        public float GetValue(CharacterStatValue stat)
        {
            return stat.Value;
        }

        public CharacterStatValue SetValue(float newValue)
        {
            return new CharacterStatValue
            {
                Value = newValue
            };
        }
    }
}