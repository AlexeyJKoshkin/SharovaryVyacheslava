using System;
using Core.Launcher;

namespace RoyalAxe.CoreLevel 
{
    public interface ILoseLevelUICommand : IUICommand
    {
    }

    public class LoseLevelUICommand : ILoseLevelUICommand
    {
        private IUICommand.UIHandler _handler;
        private readonly LoseWindowView _loseWindowView;
        private readonly IStopCoreGameLogicCommand _stopCoreGameLogicCommand;
        private readonly ICoreGameHandlerAdapter _coreGameHandlerAdapter;
        public LoseLevelUICommand(LoseWindowView loseWindowView,
                                  IStopCoreGameLogicCommand stopCoreGameLogicCommand,
                                  ICoreGameHandlerAdapter coreGameHandlerAdapter)
        {
            _loseWindowView = loseWindowView;
            _stopCoreGameLogicCommand = stopCoreGameLogicCommand;
            _coreGameHandlerAdapter = coreGameHandlerAdapter;
            _loseWindowView.LoadMetaBtn.onClick.AddListener(LoadMetaScene);
            _loseWindowView.RetryBtn.onClick.AddListener(RetryHandler);
            _loseWindowView.Open();
        }

        private void RetryHandler()
        {
            ClearWindowHandlers();
            _loseWindowView.Hide();
            _handler.FireCallback(true);
        }

        public void ExecuteCommand(Action<bool> onDoneExecuteCommand = null)
        {
            _handler = new IUICommand.UIHandler(onDoneExecuteCommand);
            _stopCoreGameLogicCommand.StopGameLogic();
            _loseWindowView.Open();
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