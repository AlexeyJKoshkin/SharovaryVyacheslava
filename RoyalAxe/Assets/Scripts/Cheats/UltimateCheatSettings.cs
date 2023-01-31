using Core.UserProfile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class UltimateCheatSettings : ScriptableObject
    {
        public bool EnableCheats;

        [EnableIf("EnableCheats")] public bool EnableRender = false;
        /*[EnableIf("EnableCheats")]
        [BoxGroup("Start Level")]
        public bool StartCustomLevel;
        [EnableIf("EnableCheats")]
        [EnableIf("StartCustomLevel")]
        [BoxGroup("Start Level")]
        public LastLevel LevelParams;*/
    }
}
