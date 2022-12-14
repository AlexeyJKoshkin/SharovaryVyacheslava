using System;
using UnityEngine;

namespace Core.UserProfile
{
    [Serializable]
    public class PCPlayer : UserSavePathSettings
    {
        public override string RootPath => $"{Application.persistentDataPath}/[SharovaruVaycheslava_UserProfile]/";

        public override RuntimePlatform Id => RuntimePlatform.WindowsPlayer;
    }
}