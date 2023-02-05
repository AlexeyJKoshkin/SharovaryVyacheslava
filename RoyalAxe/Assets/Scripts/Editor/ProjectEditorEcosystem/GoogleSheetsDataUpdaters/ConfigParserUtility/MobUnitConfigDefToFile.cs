using System.Collections.Generic;
using System.Linq;
using Core.EditorCore.Parser;
using Core.Parser;
using RoyalAxe.Configs;
using RoyalAxe.Units.Stats;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    internal class MobUnitConfigDefToFile : ModelsToJsonFile<MobUnitJsonData>
    {
        protected override IEnumerable<MobUnitJsonData> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser)
        {
            yield return SingleCollection(googleSheetGameData, dataParser);
        }
        
        protected MobUnitJsonData SingleCollection(GoogleSheetGameData page, IGameDataParser parser)
        {
            MobUnitJsonData result = new MobUnitJsonData()
            {
                UniqueID = page.PageName,
                StatCollection = new List<StatsConfig>(),
                MobWeaponData = new MobWeaponSkillConfigDef()
            };
            
            for (int i = 0; i < page.Cells.Count; i++)
            {
                var lvlCells = page.Cells[i];
                AddToListData(result.StatCollection,parser, lvlCells);
                AddToListData(result.MobWeaponData.RangeConfig,parser, lvlCells);
                AddToListData(result.MobWeaponData.SkillDamage,parser, lvlCells);
            }
            return result;
        }
    }

    internal class CharacterHeroConfigDefToFile : ModelsToJsonFile<HeroUnitJsonData>
    {
        protected override IEnumerable<HeroUnitJsonData> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser)
        {
            yield return SingleCollection(googleSheetGameData, dataParser);
        }
        
        protected HeroUnitJsonData SingleCollection(GoogleSheetGameData page, IGameDataParser parser)
        {
            HeroUnitJsonData result = new HeroUnitJsonData()
            {
                UniqueID       = page.PageName,
                StatCollection = new List<StatsConfig>()
            };
            
            for (int i = 0; i < page.Cells.Count; i++)
            {
                var lvlCells = page.Cells[i];
                AddToListData(result.StatCollection,parser, lvlCells);
            }
            return result;
        }
    }
}