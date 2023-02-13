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

        protected override void UpdateJson(List<GoogleSheetGameData> allPages, IProjectEditorUtility currentUtility)
        {
            IJsonConfigModelsOperation operation = currentUtility.ConfigOperation;
            var genericParser = new CompositeGenericParser().BindWeaponSkills().Bind<UnitWeaponSkillConfigDef>();
            var modelsToJsonHelper = new ModelsToJsonHelper<UnitWeaponSkillConfigDef>(operation);
            var items = new List<UnitWeaponSkillConfigDef>();
            allPages.ForEach(p => //распарсили каждую страничку и добавили их в общий список
            {
                var newItemsFromPage = SingleCollection(p, genericParser);
                items.Add(newItemsFromPage);
            });
            
            modelsToJsonHelper.UpdateModels(items);
        }
        
        protected UnitWeaponSkillConfigDef SingleCollection(GoogleSheetGameData page, IGameDataParser parser)
        {
            UnitWeaponSkillConfigDef result = new UnitWeaponSkillConfigDef(page.Cells.Count)
            {
                UniqueID = page.PageName
            };
            for (int i = 0; i < page.Cells.Count; i++)
            {
                var lvlCells = page.Cells[i];                        // тут содержится инфа об уровне оружия
                result.SkillDamage.AddParsedData(parser, lvlCells); 
                result.RangeConfig.AddParsedData(parser, lvlCells);
            }
            return result;
        }
    }
}