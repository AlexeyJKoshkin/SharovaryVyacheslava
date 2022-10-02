using System.Collections.Generic;
using Core.EditorCore.Parser;
using GameKit;
using GameKit.Editor;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    public class ExampleGoogleSheetUpdaterConfigToScriptable : IGoogleSheetDataToGameConfigConverter
    {
        private const string StatsFolderName = "Stats";
        private const string DefaultWeaponsFolderName = "DefaultWeapon";
        [SerializeField] private ConfigLoadUtility _weaponDataLoader = new ConfigLoadUtility();

        private Pathes _pathes;


        private void CreateConfigInfrastructure(string rootPath, string folderId)
        {
            _pathes = new Pathes
            {
                EntityPath        = $"{rootPath}/{folderId}",
                StatsPath         = $"{rootPath}/{folderId}/{StatsFolderName}",
                DefaultWeaponPath = $"{rootPath}/{folderId}/{DefaultWeaponsFolderName}"
            };

            _pathes.All().ForEach(e => EditorUtils.CreateAssetFolder(e));
        }

        private struct Pathes
        {
            public string EntityPath;
            public string DefaultWeaponPath;
            public string StatsPath;

            public IEnumerable<string> All()
            {
                yield return EntityPath;
                yield return DefaultWeaponPath;
                yield return StatsPath;
            }
        }

        public void ParseSheetData(IEnumerable<GoogleSheetGameData> data)
        {
            data.ForEach(ParseSheetData);
        }

        private void ParseSheetData(GoogleSheetGameData data)
        {
            var rootPath = _weaponDataLoader.RootPath;
            if (string.IsNullOrEmpty(rootPath))
            {
                return;
            }

            CreateConfigInfrastructure(rootPath, data.PageName);
            //_statDataLoader.UpdateDataBox(_pathes.StatsPath);  // обновляю все датабоксы конфигами из папки
            //   _weaponDataLoader.UpdateDataBox(_pathes.DefaultWeaponPath);
        }
    }
}