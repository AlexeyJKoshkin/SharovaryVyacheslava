namespace RoyalAxe.Units.Stats
{
    public interface IStatValueProvider
    {
        float GetValue(CharacterStatValue stat);
        CharacterStatValue SetValue(float newValue);
    }
}