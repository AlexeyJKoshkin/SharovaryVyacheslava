using System;
using System.Collections.Generic;
using System.Linq;
using Core.EditorCore.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters {
    [Serializable]
    public abstract class RAGoogleSheetDataToGameConfigConverter : IGoogleSheetDataToGameConfigConverter
    {
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
        }

        protected abstract void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility);
    }
}