namespace RoyalAxe.CharacterStat
{
    public interface IGameStatChangeable
    {
        IChangeModificatorBuilder ChangeValue();

        IChangeModificatorBuilder ChangeMaxValue();
    }
}