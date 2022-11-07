using Core.Installers;

namespace RoyalAxe.CoreLevel
{
    public interface IStopCoreGameLogicCommand
    {
        void StopGameLogic();
    }
    
    public class StopCoreGameLogicCommand : IStopCoreGameLogicCommand
    {
        private readonly IMobSpawnTimer _mobSpawnTimer;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSwitcher;
        public StopCoreGameLogicCommand(IRoyalAxePauseSystemSwitcher pauseSwitcher, IMobSpawnTimer mobSpawnTimer)
        {
            _pauseSwitcher = pauseSwitcher;
            _mobSpawnTimer = mobSpawnTimer;
        }

        public void StopGameLogic()
        {
            _mobSpawnTimer.StopMobTimer();
            _pauseSwitcher.SetPause(); // все ставим 
        }
    }
}
