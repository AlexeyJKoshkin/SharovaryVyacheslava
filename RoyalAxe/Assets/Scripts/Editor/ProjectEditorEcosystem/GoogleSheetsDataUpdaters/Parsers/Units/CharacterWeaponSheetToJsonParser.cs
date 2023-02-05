using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.EditorCore.Parser;
using Core.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using RoyalAxe.Units.Stats;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    [Serializable]
    public class CharacterWeaponSheetToJsonParser : RAGoogleSheetDataToGameConfigConverter
    {
        protected override void BindParserTypes(CompositeGenericParser genericParser)
        {
            genericParser.Bind<UnitWeaponSkillConfigDef>();
            genericParser.BindWeaponSkills();
        }

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility, IGameDataParser parser)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            new HeroWeaponsSkillConfigDefToFile().UpdateConfigs(allPages, operation, parser);
        }
    }
}
