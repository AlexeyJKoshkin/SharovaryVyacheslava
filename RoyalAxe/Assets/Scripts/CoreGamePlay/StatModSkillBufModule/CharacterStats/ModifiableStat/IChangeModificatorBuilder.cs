namespace RoyalAxe.CharacterStat
{
    public interface IChangeModificatorBuilder : ICharacterStatModificator
    {
        IChangeModificatorBuilder ByConstValue(float constValue);

        IChangeModificatorBuilder FromNativeCurrent(float percent);

        IChangeModificatorBuilder FromNative(ModificatorChangeValueType percentRefType, float percent);
        IChangeModificatorBuilder FromActualMax(float percent);
        IChangeModificatorBuilder FromNativeMax(float percent);
        IChangeModificatorBuilder FromActualCurrent(float percent);
        IChangeModificatorBuilder FromActual(ModificatorChangeValueType percentRefType, float percent);
    }
}