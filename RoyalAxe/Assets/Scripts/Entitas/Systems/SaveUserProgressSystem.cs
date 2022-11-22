using Core.UserProfile;
using Entitas;

namespace RoyalAxe.EntitasSystems {
    public class SaveUserProgressSystem : IExecuteSystem
    {
        private readonly IUserSaveProfileStorage _userSaveProfileStorage;
        public SaveUserProgressSystem(IUserSaveProfileStorage userSaveProfileStorage)
        {
            _userSaveProfileStorage = userSaveProfileStorage;
        }

        public void Execute()
        {
            _userSaveProfileStorage.Save();
        }
    }
}