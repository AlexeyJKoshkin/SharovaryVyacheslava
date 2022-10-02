using System.Linq;
using UnityEngine;

namespace Core.UserProfile
{
    public interface IUserSavePathSettings
    {
        string RootPath { get; }
        RuntimePlatform Id { get; }
    }

    public class UserSaveSettingStorage : ScriptableObject
    {
        [SerializeReference] private IUserSavePathSettings[] _pathSettingses;

        public IUserSavePathSettings GetCurrentSettings()
        {
            return _pathSettingses.FirstOrDefault(o => o.Id == Application.platform);
        }
    }
}