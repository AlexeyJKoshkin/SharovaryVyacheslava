using Core.UserProfile;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace RoyalAxe.CoreLevel
{
    public interface IUltimateCheatAdapter
    {
        bool EnableRender { get; }
        bool UseLevelFromCheat { get; }
        LastLevel LevelParams { get; }
    }

    public class UltimateCheatStarter : IUltimateCheatAdapter, IInitializable
    {
        private readonly UltimateCheatSettings _cheatSettings;
        private readonly GameRootLoopContext _rootLoopContext;
        public bool EnableRender => !_rootLoopContext.hasCheats || _cheatSettings.EnableRender;
        public bool UseLevelFromCheat => _cheatSettings.EnableCheats && _cheatSettings.StartCustomLevel;
        public LastLevel LevelParams => _cheatSettings.LevelParams;

        public UltimateCheatStarter(UltimateCheatSettings cheatSettings, GameRootLoopContext rootLoopContext)
        {
            _cheatSettings = cheatSettings;
            _rootLoopContext = rootLoopContext;
        }

        public void Initialize()
        {
            if(!_cheatSettings.EnableCheats) return;
            _rootLoopContext.ReplaceCheats(_cheatSettings);
        }
    }

    public class UltimateCheatSettings : ScriptableObject
    {
        public bool EnableCheats;

        [EnableIf("EnableCheats")] public bool EnableRender = false;
        [EnableIf("EnableCheats")]
        [BoxGroup("Start Level")]
        public bool StartCustomLevel;
        [EnableIf("EnableCheats")]
        [EnableIf("StartCustomLevel")]
        [BoxGroup("Start Level")]
        public LastLevel LevelParams;
    }
}
