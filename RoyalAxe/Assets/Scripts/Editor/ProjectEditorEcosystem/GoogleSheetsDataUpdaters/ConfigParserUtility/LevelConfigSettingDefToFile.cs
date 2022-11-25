using System.Collections.Generic;
using System.Linq;
using Core.EditorCore.Parser;
using Core.Parser;
using RoyalAxe.CoreLevel;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    internal class LevelConfigSettingDefToFile : ModelsToJsonFile<LevelSettingsData>
    {
        readonly MobSpawnConfigData _mobSpawnParserHelper = new MobSpawnConfigData();
        
        public LevelConfigSettingDefToFile()
        {
            Bind<LevelSettingsData>();
            Bind<MobDeathReward>();
            Bind<WaveDestiny>();
        }

        protected override void RemoveUpdateConfigs(List<LevelSettingsData> allExistItems, List<GoogleSheetGameData> allPages)
        {
            allExistItems.Clear();
        }

        protected override IEnumerable<LevelSettingsData> Parse(GoogleSheetGameData page, IGameDataParser parser)
        {
            for (int i = 0; i < page.Cells.Count; i++)
            {
                LevelSettingsData result = new LevelSettingsData();
                var levelCells = page.Cells[i];                       // тут содержится инфа об волнах уровня
                parser.UpdateObject(levelCells, result);              // общий класс
                parser.UpdateObject(levelCells, result.MobDeathReward); // мобьи награды
                parser.UpdateObject(levelCells, result.Destiny);        // судьба/магазин
                FillMobSpawnData(levelCells, result);
                yield return result;
            }
        }

      
        private const int MaxDifferentMobs = 5;

        private void FillMobSpawnData(List<ICellValue> levelCells, LevelSettingsData result)
        {
            var neededCells = levelCells.Where(o => o.ColumnName.Contains(_mobSpawnParserHelper.BlankMobAmount) ||
                                                    o.ColumnName.Contains(_mobSpawnParserHelper.BlankMobId) ||
                                                    o.ColumnName.Contains(_mobSpawnParserHelper.BlankMobLevel)).ToList();
            for (int i = 0; i < MaxDifferentMobs; i++)
            {
                _mobSpawnParserHelper.Set(i + 1);
                var mobIdCell = FindAndRemove(neededCells,_mobSpawnParserHelper.MobIdCell);
                if(mobIdCell == null || string.IsNullOrEmpty(mobIdCell.Value)) break; // не указан моб, значит все остальное игнорируем
                var levelIdCell = FindAndRemove(neededCells, _mobSpawnParserHelper.MobIdLevel);
                var amountCell = FindAndRemove(neededCells,_mobSpawnParserHelper.MobIdAmount);
                result.MobsData.Add(new MobAtLevelData()
                {
                    MobId = mobIdCell.Value,
                    TotalAmount = (byte)CommonTypeParser.ParseInt(amountCell.Value),
                    Level = (byte)CommonTypeParser.ParseInt(levelIdCell.Value)
                });
            }
        }

        private ICellValue FindAndRemove(List<ICellValue> neededCells, string columnName)
        {
            var result = neededCells.FirstOrDefault(o => o.ColumnName == columnName);
            neededCells.Remove(result);
            return result;
        }

        class MobSpawnConfigData
        {
            public string BlankMobId = "Enemy";
            public string BlankMobLevel = "Level_enemy";
            public string BlankMobAmount = "Count_enemy";

            public string MobIdCell;
            public string MobIdLevel;
            public string MobIdAmount;

            public void Set(int i)
            {
                MobIdCell = $"{BlankMobId}_{i}";
                MobIdLevel = $"{BlankMobLevel}_{i}";
                MobIdAmount = $"{BlankMobAmount}_{i}";
            }
        }
    }
}
