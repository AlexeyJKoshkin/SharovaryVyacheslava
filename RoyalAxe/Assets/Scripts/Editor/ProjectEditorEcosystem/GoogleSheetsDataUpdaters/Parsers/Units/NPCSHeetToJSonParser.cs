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

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            new WizardShopConfigDefToFile().UpdateConfigs(allPages, operation,null);
        }
    }
}