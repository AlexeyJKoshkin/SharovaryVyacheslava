using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.Data.Provider;
using Core.EditorCore.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters 
{
    [Serializable]
    public abstract class UnitSheetToJsonParser<TUnitScriptable> : RAGoogleSheetDataToGameConfigConverter  where TUnitScriptable : ScriptableObject, IDataObject
    {
        [SerializeField] private ConfigLoadUtility _unitsDataLoader = new ConfigLoadUtility();

      

        private void UpdateScriptable(List<GoogleSheetGameData> allPages)
        {
            var rootPath = _unitsDataLoader.RootPath;
            if (string.IsNullOrEmpty(rootPath)) return;

            allPages.ForEach(p => { _unitsDataLoader.GetById<TUnitScriptable>(p.PageName); });
            _unitsDataLoader.UpdateDataBox();
        }

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            new WeaponsSkillConfigDefToFile().UpdateConfigs(allPages, operation);
            new StatsConfigDefToFile().UpdateConfigs(allPages, operation);
            
            UpdateScriptable(allPages);
        }
    }
}