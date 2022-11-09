using Core.Installers;

namespace RoyalAxe.CoreLevel
{
    public interface IStopCoreGameLogicCommand
    {
        void StopGameLogic();
    }
    
    public class StopCoreGameLogicCommand : IStopCoreGameLogicCommand
    {
        private readonly IMobSpawnFacade _mobSpawnFacade;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSwitcher;
        public StopCoreGameLogicCommand(IRoyalAxePauseSystemSwitcher pauseSwitcher, IMobSpawnFacade mobSpawnFacade)
        {
            _pauseSwitcher = pauseSwitcher;
            _mobSpawnFacade = mobSpawnFacade;
        }

        public void StopGameLogic()
        {
            _mobSpawnFacade.StopSpawn();
            _pauseSwitcher.SetPause(); // все ставим 
        }
    }
}
