using Core.Configs;
using RoyalAxe.Units.Stats;
using RoyalAxe.Configs;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelSkill;
using UnityEngine;
using VContainer;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/Configs", fileName = "ConfigsInstaller")]
    public class ConfigsScriptableInstaller : ScriptableInstaller
    {
        [SerializeField]
        private BuildInJsonDataProvider _buildInJsonDataProvider;
        [SerializeField]
        private UltimateCheatSettings _cheatSettings;
        protected override void InstallBindings()
        {
            Container.Register<JsonModelsDataBox<WeaponsSkillConfigDef>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<JsonModelsDataBox<StatCollection>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<JsonModelsDataBox<WizardLevelCollection>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<JsonModelsDataBox<LevelSettingsData>>(Lifetime.Singleton).AsImplementedInterfaces();

            BindJsonFileDependencies();

            Container.Register<LevelBuffSettingCompositeProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            BindCheats();
        }
        private void BindJsonFileDependencies()
        {
            Container.Register<SVJsonConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<TextFileOperation>(Lifetime.Singleton).AsImplementedInterfaces();              // для сохранения загрузки стрингов json
            Container.Register<JsonConfigsModelOperation>(Lifetime.Singleton).AsImplementedInterfaces(); // грузильщик конфигов из json
            Container.Register<JsonConfigsPathBuilder>(Lifetime.Singleton).AsImplementedInterfaces();         // прокладывает путь к файлам
            
            Container.RegisterInstance(_buildInJsonDataProvider).AsSelf();
            
            Container.Register<BuildInJsonConfigLoader>(Lifetime.Singleton).As<IJsonConfigFileLoader>();
            
            //Container.Register<ConfigTextLoader>(Lifetime.Singleton).As<IJsonConfigFileLoader>();

        }
        
        private void BindCheats()
        {
            Container.Register<UltimateCheatStarter>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.RegisterInstance(_cheatSettings).AsSelf();
        }

    }
}
