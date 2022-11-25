namespace RoyalAxe.CharacterStat
{
    public interface IStatValueProvider
    {
        float GetValue(CharacterStatValue stat);
        CharacterStatValue SetValue(float newValue);
    }
}