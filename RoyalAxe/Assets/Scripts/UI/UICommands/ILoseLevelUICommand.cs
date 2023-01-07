using System;
using Core.Installers;
using Core.Launcher;
using FluentBehaviourTree;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.UI
{
 

    public class LoseLevelUIScenario : UIScenario<LoseWindowView>
    {
        private readonly IRoyalAxePauseSystemSwitcher _pauseSystemSwitcher;
        private readonly IStopCoreGameLogicCommand _stopCoreGameLogicCommand;
        private readonly ICoreGameHandlerAdapter _coreGameHandlerAdapter;
        private readonly IResetCoreGameToRetryCommand _resetCoreGame;
        public LoseLevelUIScenario(LoseWindowView loseWindowView,
                                  IStopCoreGameLogicCommand stopCoreGameLogicCommand,
                                  ICoreGameHandlerAdapter coreGameHandlerAdapter,
                                  IResetCoreGameToRetryCommand resetCoreGame,
                                  IRoyalAxePauseSystemSwitcher pauseSystemSwitcher)
        {
            _stopCoreGameLogicCommand = stopCoreGameLogicCommand;
            _coreGameHandlerAdapter = coreGameHandlerAdapter;
            _resetCoreGame = resetCoreGame;
            _pauseSystemSwitcher = pauseSystemSwitcher;
            InitView(loseWindowView);
        }

        public override void InitView(LoseWindowView view)
        {
            base.InitView(view);
            view.LoadMetaBtn.onClick.AddListener(LoadMetaScene);
            view.RetryBtn.onClick.AddListener(RetryHandler);
        }

        public override void EnterState()
        {
            _stopCoreGameLogicCommand.StopGameLogic();
            View.Open();
        }


        private void RetryHandler()
        {
            ClearWindowHandlers();
            _resetCoreGame.RestartGameAfterPlayerDearth();
            _pauseSystemSwitcher.UnPause();
            View.Close();
        }
        
        private void LoadMetaScene()
        {
            ClearWindowHandlers();
        
            _coreGameHandlerAdapter.LoadMetaScene();
        }

        private void ClearWindowHandlers()
        {
            View.LoadMetaBtn.onClick.RemoveAllListeners();
            View.RetryBtn.onClick.RemoveAllListeners();
        }
    }
}