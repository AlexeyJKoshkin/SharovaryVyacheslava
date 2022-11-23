namespace Core.UserProfile
{
    /// <summary>
    /// Общий прогресс профиля
    /// </summary>
    public interface IGeneralProfileProgress : IUserProgressProfile
    {
        
    }

    public class EnergyProgressSaveData
    {
        public int CurrentEnergy;
    }

    class GeneralProfileProgress
    {
        public ProfileWalletProgress Wallet;
        public EnergyProgressSaveData Energy;
        public int ProfileLevel;
    }

    public class ProfileWalletProgress : BaseUserProgressData
    {
        public int SoftCurrency;
        public int HardCurrency;
    }
}
