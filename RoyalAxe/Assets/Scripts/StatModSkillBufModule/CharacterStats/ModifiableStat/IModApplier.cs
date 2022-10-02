namespace RoyalAxe.CharacterStat
{
    public interface IModApplier
    {
        ICharacterStatModificator ApplyMod();
        ICharacterStatModificator ApplyPermanentMod();
    }
}