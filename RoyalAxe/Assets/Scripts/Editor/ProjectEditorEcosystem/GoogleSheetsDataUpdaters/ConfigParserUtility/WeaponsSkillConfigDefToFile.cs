using System.Collections.Generic;
using Core.EditorCore.Parser;
using Core.Parser;
using RoyalAxe.CharacterStat;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    internal class WeaponsSkillConfigDefToFile : ModelsToJsonFile<WeaponsSkillConfigDef>
    {
        public WeaponsSkillConfigDefToFile()
        {
            Bind<SkillConfigDef.RangeParams>().Bind<SkillConfigDef.Damage>();
        }
        
        protected override IEnumerable<WeaponsSkillConfigDef> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser)
        {
            yield return SingleCollection(googleSheetGameData, dataParser);
        }

        protected WeaponsSkillConfigDef SingleCollection(GoogleSheetGameData page, IGameDataParser parser)
        {
            WeaponsSkillConfigDef result = new WeaponsSkillConfigDef(page.PageName, page.Cells.Count);

            for (int i = 0; i < page.Cells.Count; i++)
            {
                var lvlCells = page.Cells[i];                       // тут содержится инфа об уровне оружия
                AddToListData(result.SkillDamage, parser, lvlCells); // Добавили уровень
                AddToListData(result.RangeConfig, parser, lvlCells);
            }

            return result;
        }
    }
}