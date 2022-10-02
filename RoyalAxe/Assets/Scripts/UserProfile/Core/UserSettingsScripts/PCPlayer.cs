using System;
using UnityEngine;

namespace Core.UserProfile
{
    [Serializable]
    public class PCPlayer : UserSavePathSettings
    {
        public PCPlayer()
        {
            RootPath = $"{Application.streamingAssetsPath}/[SharovaruVaycheslava_UserProfile]/";
        }

        public override RuntimePlatform Id => RuntimePlatform.WindowsPlayer;
    }
}