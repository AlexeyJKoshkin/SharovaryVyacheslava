namespace Core.UserProfile
{
    class GeneralProfileProgressDefaultFactory :  IDefaultProgressFactory<GeneralProfileProgress>
    {
        public GeneralProfileProgress CreateDefault()
        {
            return new GeneralProfileProgress()
            {
                ProfileLevel = 1,
                Wallet = new ProfileWalletProgress(){SoftCurrency = 100, HardCurrency = 100}
            };
        }
    }
}
