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
    public class EnemyUnitSheetToJsonParser : RAGoogleSheetDataToGameConfigConverter
    {
        [SerializeField] private ConfigLoadUtility _unitsDataLoader = new ConfigLoadUtility();


        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            new WeaponsSkillConfigDefToFile().UpdateConfigs(allPages, operation);
            new StatsConfigDefToFile().UpdateConfigs(allPages, operation);
            
            UpdateScriptable(allPages);
        }
        
        private void UpdateScriptable(List<GoogleSheetGameData> allPages)
        {
            var rootPath = _unitsDataLoader.RootPath;
            if (string.IsNullOrEmpty(rootPath)) return;

            allPages.ForEach(p => { _unitsDataLoader.GetById<UnitConfigDef>(p.PageName); });
            _unitsDataLoader.UpdateDataBox();
        }
    }
}