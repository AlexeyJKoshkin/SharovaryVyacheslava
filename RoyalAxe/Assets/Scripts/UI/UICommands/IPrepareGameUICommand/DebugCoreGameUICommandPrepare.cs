using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel 
{
    public class DebugCoreGameUICommandPrepare : IPrepareGameUICommand
    {
        private readonly CoreDebugWaveInfoUIView _debugWave;
        private readonly ILevelBuffStorage _levelBuffStorage;
        private readonly Contexts _contexts;
        public DebugCoreGameUICommandPrepare(Contexts contexts, CoreDebugWaveInfoUIView debugWave, ILevelBuffStorage levelBuffStorage)
        {
            _contexts  = contexts;
            _debugWave = debugWave;
            _levelBuffStorage = levelBuffStorage;
        }

        public void PrepareUIStartGame()
        {
            if (_contexts.gameRootLoop.hasCheats)
            {
                _debugWave.InitEntity(_contexts.coreGamePlay.levelWaveEntity);
                _debugWave.PlayerLevelPowerView.Init(_levelBuffStorage);
            }
            else
            {
                _debugWave.Close();
            }
            
        }
    }
}