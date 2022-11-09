using System;
using Core.Installers;

namespace RoyalAxe.CoreLevel
{
    public interface IShowSelectBuffWindowCommand : IUICommand { }

    public class ShowSelectBuffWindowCommand : IShowSelectBuffWindowCommand
    {
        private readonly ISelectBuffScenario _buffScenario;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSystemSwitcher;
        private readonly IMobSpawnFacade _mobSpawnFacade;
        private UIHandler _handler;

        public ShowSelectBuffWindowCommand(ISelectBuffScenario buffScenario, IRoyalAxePauseSystemSwitcher pauseSystemSwitcher, IMobSpawnFacade mobSpawnFacade)
        {
            _buffScenario = buffScenario;
            _pauseSystemSwitcher = pauseSystemSwitcher;
            _mobSpawnFacade = mobSpawnFacade;
        }

        public void ExecuteCommand(Action<bool> onDoneExecuteCommand = null)
        {
            _handler = new UIHandler(onDoneExecuteCommand);
            _pauseSystemSwitcher.SetPause();
            _buffScenario.OnFinishScenarioEvent += BuffScenarioOnOnFinishScenarioEvent;
            _buffScenario.DoShowExpBuffs();
        }

        private void BuffScenarioOnOnFinishScenarioEvent(bool isSuccess)
        {
            _buffScenario.OnFinishScenarioEvent -= BuffScenarioOnOnFinishScenarioEvent;
            _pauseSystemSwitcher.UnPause();
            _mobSpawnFacade.StartSpawnMob();
            _handler.FireCallback(isSuccess);
        }
    }
}
