using Core.Data.Provider;
using Core.Installers;
using Core.UserProfile;
using GameKit;
using VContainer.Unity;

namespace Core.Launcher
{
    /*
    * Точка входа в игру после инициализации project Context
    */
    public class GameRootLauncher : IInitializable
    {
        private readonly IDataStorage _dataStorage;
        private readonly IUserSaveProfileStorage _userSaveProfileStorage;
        private readonly IRoyalAxeCoreSystemsBuilder _builder;
        private readonly GameRootUnityCallbackReceiver _receiver;

        public GameRootLauncher(IDataStorage dataStorage,
                                IUserSaveProfileStorage userSaveProfileStorage,
                                IRoyalAxeCoreSystemsBuilder builder,
                                GameRootUnityCallbackReceiver receiver)
        {
            _dataStorage            = dataStorage;
            _userSaveProfileStorage = userSaveProfileStorage;
            _builder                = builder;
            _receiver               = receiver;
        }

        public void Initialize()
        {
            //TODO:Инициализируем различные логические модули игры. 
            /* - Загрузчик ресурсов
             */
            InitData();
            InitUserProfile();
            _receiver.AddUpdate(_builder.BuildUpdate("[CoreSystems]"));
            //   CreateRootMonoUnity(); // создаем апдейтор логи ядра игры
        }


        private void InitUserProfile()
        {
            var currentSave = _userSaveProfileStorage.Current;
            HLogger.LogInfo($"Total {_userSaveProfileStorage.AllSaves.Count} Current : {currentSave.FolderPath.Name} Path => {currentSave.FolderPath} ");
        }

        private void InitData()
        {
            _dataStorage.ForEach(box => HLogger.LogInfo($"{box.ObjectType.Name} -> {box.Count}"));
        }
    }
}