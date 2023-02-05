using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.EditorCore.Parser;
using Core.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using RoyalAxe.Configs;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters 
{
    [Serializable]
    internal class EnemyUnitSheetToJsonParser : UnitSheetToJsonParser<UnitConfigDef,MobUnitConfigDefToFile>
    {
        protected override void BindParserTypes(CompositeGenericParser genericParser)
        {
            genericParser.BindWeaponSkills();
        }
    }
}