using System;
using System.Collections.Generic;
using Core.EditorCore.Parser;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters 
{
    [Serializable]
    public class LevelBuffToJsonParser : IGoogleSheetDataToGameConfigConverter
    {
        [SerializeField] private ConfigLoadUtility _buffDataLoader = new ConfigLoadUtility();
        
        public void ParseSheetData(IEnumerable<GoogleSheetGameData> data)
        {
            throw new NotImplementedException();
        }
    }
}