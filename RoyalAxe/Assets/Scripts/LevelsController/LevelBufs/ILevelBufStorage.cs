namespace RoyalAxe.LevelBuff
{
    public interface ILevelBuffStorage
    {
        ILevelBuff[] GenerateBuffs();

        void TempDoAll();
    }
}