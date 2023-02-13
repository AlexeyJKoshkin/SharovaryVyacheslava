using System;
using System.Collections.Generic;
using Core.Configs;
using Core.Data.Provider;
using Core.EditorCore.Parser;
using Core.Parser;
using ProjectEditorEcoSystem;
using RoyalAxe.Configs;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    [Serializable]
    public abstract class UnitSheetToJsonParser<TUnitScriptable, TConfigUpdater> : RAGoogleSheetDataToGameConfigConverter
        where TUnitScriptable : ScriptableObject, IDataObject
        where TConfigUpdater : IConfigUpdater, new()
    {
        [SerializeField] private ConfigLoadUtility _unitsDataLoader = new ConfigLoadUtility();

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            CompositeGenericParser parser = new CompositeGenericParser();
            BindParserTypes(parser);
            new TConfigUpdater().UpdateConfigs(allPages, operation,parser);
            UpdateScriptable(allPages);
        }

        protected virtual void BindParserTypes(CompositeGenericParser genericParser)
        {
            genericParser.Bind<StatsConfig>();
        }

        protected void UpdateScriptable(List<GoogleSheetGameData> allPages)
        {
            var rootPath = _unitsDataLoader.RootPath;
            if (string.IsNullOrEmpty(rootPath)) return;

            allPages.ForEach(p => { _unitsDataLoader.GetById<TUnitScriptable>(p.PageName); });
            _unitsDataLoader.UpdateDataBox();
        }
    }
}