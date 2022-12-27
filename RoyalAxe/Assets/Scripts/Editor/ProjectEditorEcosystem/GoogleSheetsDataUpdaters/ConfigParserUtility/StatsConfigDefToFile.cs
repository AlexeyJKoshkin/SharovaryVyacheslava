using System.Collections.Generic;
using System.Linq;
using Core.EditorCore.Parser;
using Core.Parser;
using RoyalAxe.Configs;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    internal class StatsConfigDefToFile : ModelsToJsonFile<StatCollection>
    {
        public StatsConfigDefToFile()
        {
            Bind<StatsConfig>();
        }

        protected StatCollection SingleCollection(GoogleSheetGameData page, IGameDataParser parser)
        {
            StatCollection result = new StatCollection {UniqueID = page.PageName};

            for (int i = 0; i < page.Cells.Count; i++)
            {
                var lvlCells = page.Cells[i]; // тут содержится инфа об уровне оружия
                var item =  parser.UpdateObject(lvlCells, new StatsConfig()) as StatsConfig;
                result.Stats.Add(item);
            }

            return result;
        }

        protected override void RemoveUpdateConfigs(List<StatCollection> allExistItems, List<GoogleSheetGameData> allPages)
        {
            allExistItems.RemoveAll(o => allPages.Any(p => p.PageName == o.UniqueID)); //удаляем все модели которые будем обновлять
        }

        protected override IEnumerable<StatCollection> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser)
        {
            yield return SingleCollection(googleSheetGameData, dataParser);
        }
    }
}