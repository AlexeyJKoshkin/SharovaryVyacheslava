using UnityEngine;

namespace Core.UserProfile
{
    public class GeneralProfileProgressSaveLoaderAdapter : UserProfileProgressSaveLoaderAdapter<GeneralProfileProgress>, IGeneralProfileProgress, IWalletProfile
    {
        public int Gold => Progress.Wallet.SoftCurrency;
        public int Gems => Progress.Wallet.HardCurrency;

        public IWalletProfile Wallet => this;
        public int ProfileLevel => Progress.ProfileLevel;

        public GeneralProfileProgressSaveLoaderAdapter(IUserProgressPartFactory<GeneralProfileProgress> loader) : base(loader) { }
        protected override string Key => "Common";

        protected override void SetToMainFacade(GeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            currentGeneralUserProgressProfileFacade.GeneralProgress = this;
        }

        void IWalletProfile.AddGems(int delta)
        {
            Progress.Wallet.HardCurrency = Mathf.Max(0, delta + Gems);
            SetDirty();
        }

        void IWalletProfile.AddGold(int delta)
        {
            Progress.Wallet.SoftCurrency = Mathf.Max(0, delta + Gold);
            SetDirty();
        }
    }
}
