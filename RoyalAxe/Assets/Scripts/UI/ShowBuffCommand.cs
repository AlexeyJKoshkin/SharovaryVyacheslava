using Core.Installers;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel 
{
    public class ShowBuffCommand : IShowBuffCommand
    {
        private readonly BuffSelectWindowView _buffSelectWindowView;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSystemSwitcher;
        private readonly ILevelBuffStorage _buffStorage;

        public ShowBuffCommand(BuffSelectWindowView buffSelectWindowView, IRoyalAxePauseSystemSwitcher pauseSystemSwitcher, ILevelBuffStorage buffStorage)
        {
            _buffSelectWindowView = buffSelectWindowView;
            _pauseSystemSwitcher  = pauseSystemSwitcher;
            _buffStorage          = buffStorage;
        }

        public void DoShowExpBuffs()
        {
            _pauseSystemSwitcher.SetPause();
            LevelBuffHandler handler = new LevelBuffHandler(_buffStorage,_buffSelectWindowView,ContinueGame);
            handler.ShowWindow();
        }

        void ContinueGame()
        {
            _pauseSystemSwitcher.UnPause();
            _buffSelectWindowView.Hide();
        }
    }
}