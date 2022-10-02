using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.EditorCore.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using RoyalAxe.Configs;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters 
{
    [Serializable]
    public class HeroCharacterUnitSheetToJsonParser : IGoogleSheetDataToGameConfigConverter
    {
        [SerializeField] private ConfigLoadUtility _unitsDataLoader = new ConfigLoadUtility();

        public void ParseSheetData(IEnumerable<GoogleSheetGameData> sheet)
        {
            if (sheet == null) return;

            var launcher = EditorUtils.FindAsset<ProjectEditorEcosystemLauncher>();

            if (launcher == null || launcher.Current == null)
            {
                Debug.LogError("Eco system Not Found");
                return;
            }

            var allPages = sheet.ToList();
            UpdateJson(allPages, launcher.Current.Utility);
            UpdateScriptable(allPages);
        }

        private void UpdateScriptable(List<GoogleSheetGameData> allPages)
        {
            var rootPath = _unitsDataLoader.RootPath;
            if (string.IsNullOrEmpty(rootPath)) return;

            allPages.ForEach(p => { _unitsDataLoader.GetById<PlayerCharacterConfigDef>(p.PageName); });
            _unitsDataLoader.UpdateDataBox();
        }

        private void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            //new WeaponsSkillConfigDefToFile().UpdateConfigs(allPages, operation);
            new StatsConfigDefToFile().UpdateConfigs(allPages, operation);
        }
    }
}