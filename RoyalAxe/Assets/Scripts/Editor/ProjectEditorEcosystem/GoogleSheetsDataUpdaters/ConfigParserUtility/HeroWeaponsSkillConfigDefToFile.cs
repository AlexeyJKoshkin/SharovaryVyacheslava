using System.Collections.Generic;
using System.Linq;
using Core.EditorCore.Parser;
using Core.Parser;
using RoyalAxe.Units.Stats;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters {
    internal class HeroWeaponsSkillConfigDefToFile : ModelsToJsonFile<UnitWeaponSkillConfigDef>
    {

        protected override IEnumerable<UnitWeaponSkillConfigDef> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser)
        {
            yield return SingleCollection(googleSheetGameData, dataParser);
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
                AddToListData(result.SkillDamage, parser, lvlCells); // Добавили уровень
                AddToListData(result.RangeConfig, parser, lvlCells);
              
            }
            return result;
        }
    }
}