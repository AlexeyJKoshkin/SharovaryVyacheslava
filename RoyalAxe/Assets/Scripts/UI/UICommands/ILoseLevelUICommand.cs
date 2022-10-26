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
        private readonly IMobAtLevelDirector _mobAtLevelDirector;
        private readonly ILevelWaveLoader _levelWaveProvider;
        private readonly ICoreLevelDataInfrastructure _coreLevelDataInfrastructure;
        public LoseLevelUICommand(LoseWindowView loseWindowView,
                                  IStopCoreGameLogicCommand stopCoreGameLogicCommand,
                                  ICoreGameHandlerAdapter coreGameHandlerAdapter,
                                  IMobAtLevelDirector mobAtLevelDirector,
                                  ILevelWaveLoader levelWaveProvider,
                                  ICoreLevelDataInfrastructure coreLevelDataInfrastructure)
        {
            _loseWindowView = loseWindowView;
            _stopCoreGameLogicCommand = stopCoreGameLogicCommand;
            _coreGameHandlerAdapter = coreGameHandlerAdapter;
            _mobAtLevelDirector = mobAtLevelDirector;
            _levelWaveProvider = levelWaveProvider;
            _coreLevelDataInfrastructure = coreLevelDataInfrastructure;
            _loseWindowView.LoadMetaBtn.onClick.AddListener(LoadMetaScene);
            _loseWindowView.RetryBtn.onClick.AddListener(RetryHandler);
            _loseWindowView.Open();
        }

        private void RetryHandler()
        {
            ClearWindowHandlers();
            _levelWaveProvider.InitWaves(_coreLevelDataInfrastructure.PackLevels);
            _mobAtLevelDirector.StartWaveImmediate();
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
            _loseWindowView.LoadLastPointBtn.onClick.RemoveAllListeners();
        }
    }
}