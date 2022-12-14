using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.EditorCore.Parser;
using Core.Parser;
using ProjectEditorEcoSystem;
using RoyalAxe.LevelBuff;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters 
{
    [Serializable]
    public class LevelBuffToJsonParser : RAGoogleSheetDataToGameConfigConverter
    {
        private DataFromGoogleSheetCompositeBuilder<LevelBuffSettingsComposite> _dataParser;
        
        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            _dataParser = new DataFromGoogleSheetCompositeBuilder<LevelBuffSettingsComposite>();
            var operation = currentUtility.ConfigOperation;
            LevelBuffSettingsComposite current = new LevelBuffSettingsComposite();
            _dataParser.Load(current,allPages);
            operation.Save<LevelBuffSettingsComposite>(current);
        }
    }
}