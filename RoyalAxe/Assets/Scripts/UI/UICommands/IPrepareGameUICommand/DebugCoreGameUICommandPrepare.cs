using RoyalAxe.LevelSkill;

namespace RoyalAxe.UI
{
    public class DebugCoreGameUICommandPrepare : IPrepareGameUICommand
    {
        private readonly CoreDebugWaveInfoUIView _debugWave;
        private readonly ILevelSkillStorage _levelSkillStorage;
        private readonly Contexts _contexts;
        public DebugCoreGameUICommandPrepare(Contexts contexts, CoreDebugWaveInfoUIView debugWave, ILevelSkillStorage levelSkillStorage)
        {
            _contexts  = contexts;
            _debugWave = debugWave;
            _levelSkillStorage = levelSkillStorage;
        }

        public void PrepareUIStartGame()
        {
            /*if (_contexts.gameRootLoop.hasCheats)
            {
                _debugWave.InitEntity(_contexts.coreGamePlay.levelWaveEntity);
                _debugWave.PlayerLevelPowerView.Init(_levelSkillStorage);
            }
            else*/
            {
                _debugWave.Close();
            }
            
        }
    }
}