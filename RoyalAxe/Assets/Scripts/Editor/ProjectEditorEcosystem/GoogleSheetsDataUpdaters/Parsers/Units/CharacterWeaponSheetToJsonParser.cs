using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.EditorCore.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    [Serializable]
    public class CharacterWeaponSheetToJsonParser : RAGoogleSheetDataToGameConfigConverter
    {
      

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            new WeaponsSkillConfigDefToFile().UpdateConfigs(allPages, operation);
        }
    }
}
