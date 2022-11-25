namespace Core.UserProfile
{
    /// <summary>
    /// Общий прогресс профиля
    /// </summary>
    public interface IGeneralProfileProgress : IUserProgressProfile
    {
        IWalletProfile Wallet { get; }
        int ProfileLevel { get; }
    }


    //вообще любая валюта игрока
    public interface IWalletProfile
    {
        int Gold { get; }
        int Gems { get; }

        //todo: пока прямые методы для установки/изменению валют на баланске.
        //потом надо добавить Enum. и добавлять/удалять валюту через енум. Скорее всего так будет проще при установке наград или-что такое
        void AddGems(int delta);
        void AddGold(int delta);
    }

    public class EnergyProgressSaveData
    {
        public int CurrentEnergy;
    }

    public class GeneralProfileProgress: BaseUserProgressData
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
