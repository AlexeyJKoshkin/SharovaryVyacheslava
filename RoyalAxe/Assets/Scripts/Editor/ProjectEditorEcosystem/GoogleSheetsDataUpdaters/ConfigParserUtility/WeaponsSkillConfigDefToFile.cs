using System.Collections.Generic;
using System.Linq;
using Core.EditorCore.Parser;
using Core.Parser;
using RoyalAxe.CharacterStat;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    internal class WeaponsSkillConfigDefToFile : ModelsToJsonFile<WeaponsSkillConfigDef>
    {
        public WeaponsSkillConfigDefToFile()
        {
            Bind<SkillConfigDef.RangeParams>()
                .Bind<SkillConfigDef.Damage>()
                .Bind<SingleDamageInfo>();
        }

        protected override void RemoveUpdateConfigs(List<WeaponsSkillConfigDef> allExistItems, List<GoogleSheetGameData> allPages)
        {
            allExistItems.RemoveAll(o => allPages.Any(p => p.PageName == o.UniqueID)); //удаляем все модели которые будем обновлять
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