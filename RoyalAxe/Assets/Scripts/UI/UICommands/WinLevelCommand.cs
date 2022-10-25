using System;
using Core.Installers;
using Core.Launcher;

namespace RoyalAxe.CoreLevel 
{
    public interface IWinLevelUICommand : IUICommand
    {
      
    }

    public class WinLevelCommand : IWinLevelUICommand
    {
        public bool IsActive { get; private set; }
        
        private readonly WinWindowView _winWindowController;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSwitcher;
        private readonly ICoreGameHandlerAdapter _coreGameHandlerAdapter;
        private readonly IMobAtLevelDirector _mobAtLevelDirector;
        private IUICommand.UIHandler _handler;
        public WinLevelCommand(WinWindowView winWindowController,
                               IRoyalAxePauseSystemSwitcher pauseSwitcher,
                               ICoreGameHandlerAdapter coreGameHandlerAdapter,
                               IMobAtLevelDirector mobAtLevelDirector)
        {
            _winWindowController = winWindowController;
            _pauseSwitcher       = pauseSwitcher;
            _coreGameHandlerAdapter = coreGameHandlerAdapter;
            this._mobAtLevelDirector = mobAtLevelDirector;
        }
        public void ExecuteCommand(Action<bool> onDoneExecuteCommand = null)
        {
            IsActive = true;
            _handler = new IUICommand.UIHandler(onDoneExecuteCommand);
            _winWindowController.LoadMetaBtn.onClick.AddListener(LoadMetaScene);
            _mobAtLevelDirector.StopSpawn();
            _pauseSwitcher.SetPause();   // все ставим на паузу.
            _winWindowController.Open(); // открываем окошко победы
        }

        private void LoadMetaScene()
        {
            _winWindowController.LoadMetaBtn.onClick.RemoveAllListeners();
            _coreGameHandlerAdapter.LoadMetaScene();
            _pauseSwitcher.UnPause();
            _handler.FireCallback(true);
            IsActive = false;
        }
       
    }
}