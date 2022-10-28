using System;
using Core.Installers;
using Core.Launcher;

namespace RoyalAxe.CoreLevel 
{
    public interface ILoseLevelUICommand : IUICommand
    {
    }

    public class LoseLevelUICommand : ILoseLevelUICommand
    {
        private UIHandler _handler;
        private readonly LoseWindowView _loseWindowView;
        private IRoyalAxePauseSystemSwitcher _pauseSystemSwitcher;
        private readonly IStopCoreGameLogicCommand _stopCoreGameLogicCommand;
        private readonly ICoreGameHandlerAdapter _coreGameHandlerAdapter;
        private readonly IResetCoreGameToRetryCommand _resetCoreGame;
        public LoseLevelUICommand(LoseWindowView loseWindowView,
                                  IStopCoreGameLogicCommand stopCoreGameLogicCommand,
                                  ICoreGameHandlerAdapter coreGameHandlerAdapter, IResetCoreGameToRetryCommand resetCoreGame, IRoyalAxePauseSystemSwitcher pauseSystemSwitcher)
        {
            _loseWindowView = loseWindowView;
            _stopCoreGameLogicCommand = stopCoreGameLogicCommand;
            _coreGameHandlerAdapter = coreGameHandlerAdapter;
            _resetCoreGame = resetCoreGame;
            _pauseSystemSwitcher = pauseSystemSwitcher;
            _loseWindowView.LoadMetaBtn.onClick.AddListener(LoadMetaScene);
            _loseWindowView.RetryBtn.onClick.AddListener(RetryHandler);
            _loseWindowView.Open();
        }
        
        public void ExecuteCommand(Action<bool> onDoneExecuteCommand = null)
        {
            _handler = new UIHandler(onDoneExecuteCommand);
            _stopCoreGameLogicCommand.StopGameLogic();
            _loseWindowView.Open();
        }

        private void RetryHandler()
        {
            ClearWindowHandlers();
            _resetCoreGame.RestartGameAfterPlayerDearth();
            _pauseSystemSwitcher.UnPause();
            _loseWindowView.Hide();
            _handler.FireCallback(true);
        }
        
        private void LoadMetaScene()
        {
            ClearWindowHandlers();
        
            _coreGameHandlerAdapter.LoadMetaScene();
            _handler.FireCallback(true);
        }

        private void ClearWindowHandlers()
        {
            _loseWindowView.LoadMetaBtn.onClick.RemoveAllListeners();
            _loseWindowView.RetryBtn.onClick.RemoveAllListeners();
        }
    }
}