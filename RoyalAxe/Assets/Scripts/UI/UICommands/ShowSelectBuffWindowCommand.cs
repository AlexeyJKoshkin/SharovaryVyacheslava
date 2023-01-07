using Core.Installers;
using FluentBehaviourTree;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.UI
{
    public class ShowSelectBuffWindowCommand : IUIBehaviour
    {
        private readonly ISelectBuffScenario _buffScenario;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSystemSwitcher;

        public ShowSelectBuffWindowCommand(ISelectBuffScenario buffScenario, IRoyalAxePauseSystemSwitcher pauseSystemSwitcher, IMobSpawnFacade mobSpawnFacade)
            :base()
        {
            _buffScenario = buffScenario;
            _pauseSystemSwitcher = pauseSystemSwitcher;
            
        }
        
        public void ExitState()
        {
            _buffScenario.ExitState();
            _pauseSystemSwitcher.UnPause();
        }

        public void EnterState()
        {
            _pauseSystemSwitcher.SetPause();
            _buffScenario.EnterState();
        }

        public BehaviourTreeStatus Execute(TimeData time)
        {
            return _buffScenario.Execute(time);
        }

        public string NodeName => "Сценарий показа окна баффов";
    
    }
}
