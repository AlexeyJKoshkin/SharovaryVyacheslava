using System;
using System.Collections.Generic;
using Core.Configs;
using Core.EditorCore.Parser;
using Core.Parser;
using ProjectEditorEcoSystem;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters 
{
    [Serializable]
    public class NPCSHeetToJSonParser : RAGoogleSheetDataToGameConfigConverter
    {
        protected override void BindParserTypes(CompositeGenericParser genericParser)
        {
            
        }

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility, IGameDataParser parser)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            new WizardShopConfigDefToFile().UpdateConfigs(allPages, operation,parser);
        }
    }
}