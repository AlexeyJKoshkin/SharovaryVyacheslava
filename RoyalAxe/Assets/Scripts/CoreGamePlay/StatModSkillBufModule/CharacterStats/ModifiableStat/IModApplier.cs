namespace RoyalAxe.Units.Stats
{
    public interface IModApplier
    {
        ICharacterStatModificator ApplyMod();
        ICharacterStatModificator ApplyPermanentMod();
    }
}