using Core.Launcher;
using Core.UserProfile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public interface IUltimateCheatAdapter
    {
        bool EnableRender { get; }
        bool UseLevelFromCheat { get; }
        LastLevel LevelParams { get; }
    }

    public class UltimateCheatAdapter : IUltimateCheatAdapter
    {
        private readonly UltimateCheatSettings _cheatSettings;
        public bool EnableRender => _cheatSettings.EnableCheats && _cheatSettings.EnableRender;
        public bool UseLevelFromCheat => _cheatSettings.EnableCheats && _cheatSettings.StartCustomLevel;
        public LastLevel LevelParams => _cheatSettings.LevelParams;

        public UltimateCheatAdapter(UltimateCheatSettings cheatSettings)
        {
            _cheatSettings = cheatSettings;
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
