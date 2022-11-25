using UnityEngine;

namespace Core.UserProfile
{
    public class GeneralProfileProgressFacade : UserProgressPartFacade<GeneralProfileProgress>, IGeneralProfileProgress, IWalletProfile
    {
        public int Gold => Progress.Wallet.SoftCurrency;
        public int Gems => Progress.Wallet.HardCurrency;

        public IWalletProfile Wallet => this;
        public int ProfileLevel => Progress.ProfileLevel;

        public GeneralProfileProgressFacade(IUserProgressPartSaveLoader progressAdapter) : base(progressAdapter) { }
        protected override string Key => "Common";

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