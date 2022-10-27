using System;
using Core.Installers;
using Core.Launcher;

namespace RoyalAxe.CoreLevel
{
    public interface IWinLevelUICommand : IUICommand { }

    public class WinLevelCommand : IWinLevelUICommand
    {
        private readonly WinWindowView _winWindowController;

        private readonly ICoreGameHandlerAdapter _coreGameHandlerAdapter;
        private readonly IStopCoreGameLogicCommand _stopCoreGameLogicCommand;
        private IUICommand.UIHandler _handler;

        public WinLevelCommand(WinWindowView winWindowController,
                               ICoreGameHandlerAdapter coreGameHandlerAdapter,
                               IStopCoreGameLogicCommand stopCoreGameLogicCommand)
        {
            _winWindowController = winWindowController;
            _coreGameHandlerAdapter = coreGameHandlerAdapter;
            _stopCoreGameLogicCommand = stopCoreGameLogicCommand;
        }

        public void ExecuteCommand(Action<bool> onDoneExecuteCommand = null)
        {
            _handler = new IUICommand.UIHandler(onDoneExecuteCommand);
            _winWindowController.LoadMetaBtn.onClick.AddListener(LoadMetaScene);
            _stopCoreGameLogicCommand.StopGameLogic();
            _winWindowController.Open(); // открываем окошко победы
        }

        private void LoadMetaScene()
        {
            _winWindowController.LoadMetaBtn.onClick.RemoveAllListeners();
            _coreGameHandlerAdapter.LoadMetaScene();
            _handler.FireCallback(true);
        }
    }
}
