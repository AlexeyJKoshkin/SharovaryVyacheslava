namespace RoyalAxe.CoreLevel 
{
    public class DebugCoreGameUICommandPrepare : IPrepareGameUICommand
    {
        private readonly CoreDebugWaveInfoUIView _debugWave;
        private readonly Contexts _contexts;
        public DebugCoreGameUICommandPrepare(Contexts contexts, CoreDebugWaveInfoUIView debugWave)
        {
            _contexts  = contexts;
            _debugWave = debugWave;
        }

        public void PrepareUIStartGame()
        {
            if (_contexts.gameRootLoop.hasCheats)
            {
                _debugWave.InitEntity(_contexts.coreGamePlay.levelWaveEntity);
            }
            else
            {
                _debugWave.Close();
            }
        }
    }
}