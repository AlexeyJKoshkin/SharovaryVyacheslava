namespace Core.UserProfile
{
    internal class GeneralProfileProgressDefaultFactory : BaseDefaultProgressFactory<GeneralProfileProgress>
    {
        public override GeneralProfileProgress CreateDefault()
        {
            return new GeneralProfileProgress
            {
                ProfileLevel = 1,
                Wallet       = new ProfileWalletProgress {SoftCurrency = 100, HardCurrency = 100}
            };
        }
    }
}