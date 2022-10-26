using Core.Installers;

namespace RoyalAxe.CoreLevel
{
    public interface IStopCoreGameLogicCommand
    {
        void StopGameLogic();
    }
    
    public class StopCoreGameLogicCommand : IStopCoreGameLogicCommand
    {
        private readonly IMobAtLevelDirector _mobAtLevelDirector;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSwitcher;
        public StopCoreGameLogicCommand(IMobAtLevelDirector mobAtLevelDirector, IRoyalAxePauseSystemSwitcher pauseSwitcher)
        {
            _mobAtLevelDirector = mobAtLevelDirector;
            _pauseSwitcher = pauseSwitcher;
        }

        public void StopGameLogic()
        {
            _mobAtLevelDirector.StopSpawn();
            _pauseSwitcher.SetPause(); // все ставим 
        }
    }
}
