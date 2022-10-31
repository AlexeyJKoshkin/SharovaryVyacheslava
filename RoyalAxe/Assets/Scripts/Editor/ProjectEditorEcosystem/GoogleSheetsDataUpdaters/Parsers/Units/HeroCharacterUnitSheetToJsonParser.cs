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
    public class HeroCharacterUnitSheetToJsonParser : RAGoogleSheetDataToGameConfigConverter
    {
        [SerializeField] private ConfigLoadUtility _unitsDataLoader = new ConfigLoadUtility();

        private void UpdateScriptable(List<GoogleSheetGameData> allPages)
        {
            var rootPath = _unitsDataLoader.RootPath;
            if (string.IsNullOrEmpty(rootPath)) return;
            allPages.ForEach(p => { _unitsDataLoader.GetById<PlayerCharacterConfigDef>(p.PageName); });
            _unitsDataLoader.UpdateDataBox();
        }

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            //new WeaponsSkillConfigDefToFile().UpdateConfigs(allPages, operation);
            new StatsConfigDefToFile().UpdateConfigs(allPages, operation);
            UpdateScriptable(allPages);
        }
    }
}