using Core.Launcher;
using RoyalAxe.UI;

namespace RoyalAxe.UI {
    public class WinWindowShowScenario : UIScenario<WinWindowView>
    {
        private readonly ICoreGameHandlerAdapter _coreGameHandlerAdapter;
        private readonly IStopCoreGameLogicCommand _stopCoreGameLogicCommand;
        public WinWindowShowScenario(WinWindowView winWindowView,
                                     ICoreGameHandlerAdapter coreGameHandlerAdapter,
                                     IStopCoreGameLogicCommand stopCoreGameLogicCommand)
        {
            _coreGameHandlerAdapter = coreGameHandlerAdapter;
            _stopCoreGameLogicCommand = stopCoreGameLogicCommand;
            this.InitView(winWindowView);
        }

        public override void InitView(WinWindowView view)
        {
            base.InitView(view);
            View.LoadMetaBtn.onClick.AddListener(LoadMetaScene);
        }

        public override void EnterState()
        {
            _stopCoreGameLogicCommand.StopGameLogic();
        }

        private void LoadMetaScene()
        {
            FinishSuccess();
            View.LoadMetaBtn.onClick.RemoveAllListeners();
            _coreGameHandlerAdapter.LoadMetaScene();
        }
    }
}