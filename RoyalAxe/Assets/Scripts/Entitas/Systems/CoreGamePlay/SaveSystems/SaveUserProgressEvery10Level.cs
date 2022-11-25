using Core.UserProfile;

namespace RoyalAxe.CoreLevel
{
    public class SaveUserProgressEvery10Level : BaseSaveUserProgressAtLevels
    {
        private readonly CoreGamePlayContext _coreGamePlay;
        public SaveUserProgressEvery10Level(CoreGamePlayContext context, ICurrentUserProgressProfileFacade userProgressProfileFacade, CoreGamePlayContext coreGamePlay) : base(context,userProgressProfileFacade)
        {
            _coreGamePlay = coreGamePlay;
        }

        protected override bool Filter(CoreGamePlayEntity entity)
        {
            return entity.currentLevelInfo.Level.LevelNumber % 10 == 0; // сохраняем только на 10 уровнях
        }

        protected override void Execute(CoreGamePlayEntity e)
        {
            //сохранили уровень
            UserProgressProfileFacade.LevelProgressFacade.SavedLevel = e.currentLevelInfo.Level;

            SaveCurrency(UserProgressProfileFacade.GeneralProgress.Wallet);
           // UserProgressProfileFacade.HeroesProgress.CurrentHero
            //todo: сохранить опыт/уровень героя сохранить инвентарь
        }

        /// <summary>
        /// Добавляем заработанную валюту в профиль игрока 
        /// </summary>
        private void SaveCurrency(IWalletProfile generalProgressWallet)
        {
            generalProgressWallet.AddGems(_coreGamePlay.playerEntity.earnedGems.Value);
            generalProgressWallet.AddGold(_coreGamePlay.playerEntity.earnedGold.Value);
        }
    }
}
