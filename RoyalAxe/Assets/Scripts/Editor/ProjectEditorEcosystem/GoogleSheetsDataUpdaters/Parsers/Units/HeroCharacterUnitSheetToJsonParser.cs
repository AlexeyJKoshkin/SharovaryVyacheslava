using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.EditorCore.Parser;
using GameKit.Editor;
using ProjectEditorEcoSystem;
using RoyalAxe.Configs;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters 
{
    [Serializable]
    internal class HeroCharacterUnitSheetToJsonParser : UnitSheetToJsonParser<PlayerCharacterConfigDef,CharacterHeroConfigDefToFile>
    {
    }
}