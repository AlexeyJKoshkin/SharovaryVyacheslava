using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.EditorCore.Parser;
using Core.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using RoyalAxe.CoreLevel;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    [Serializable]
    public class LevelDataToJsonParser : RAGoogleSheetDataToGameConfigConverter
    {
        protected override void BindParserTypes(CompositeGenericParser genericParser)
        {
            genericParser.Bind<LevelSettingsData>();
        }

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility, IGameDataParser parser)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            new LevelConfigSettingDefToFile().UpdateConfigs(allPages, operation, parser);
        }
    }
}
