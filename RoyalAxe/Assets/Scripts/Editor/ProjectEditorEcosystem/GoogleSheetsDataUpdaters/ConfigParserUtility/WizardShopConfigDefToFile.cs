using System.Collections.Generic;
using System.Linq;
using Core.EditorCore.Parser;
using Core.Parser;
using GameKit;
using GameKit.Editor;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelSkill;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters {
    internal class WizardShopConfigDefToFile : ModelsToJsonFile<WizardLevelCollection>
    {
        private readonly Dictionary<string, LevelSkillType> _settingsToType = new Dictionary<string, LevelSkillType>();
        public WizardShopConfigDefToFile()
        {
            LevelBuffSettingsComposite mock = new LevelBuffSettingsComposite();
            mock.AllSettings().ForEach(e=> _settingsToType.Add(e.GetType().Name,e.Type));
        }
        
        protected override void RemoveUpdateConfigs(List<WizardLevelCollection> allExistItems, List<GoogleSheetGameData> allPages)
        {
            allExistItems.RemoveAll(o => allPages.Any(p => p.PageName == o.UniqueID)); //удаляем все модели которые будем обновлять
        }

        protected override IEnumerable<WizardLevelCollection> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser)
        {
            yield return SingleCollection(googleSheetGameData, dataParser);
        }
        
        protected WizardLevelCollection SingleCollection(GoogleSheetGameData page, IGameDataParser parser)
        {
            WizardLevelCollection result = new WizardLevelCollection {UniqueID = page.PageName};

            for (int i = 0; i < page.Cells.Count; i++)
            {
                var lvlCells = page.Cells[i];

                var item =new WizardShopSettings()
                {
                    PossibleBuffs = GetBuffsType(lvlCells).ToArray()
                };
                result.Settings.Add(item);
            }

            return result;
        }

        private IEnumerable<LevelSkillType> GetBuffsType(List<ICellValue> lvlCells)
        {
             foreach (var cell in lvlCells)
                     {
                         if (_settingsToType.TryGetValue(cell.ColumnName, out var type))
                         {
                             if (CommonTypeParser.ParseBool(cell.Value))
                                 yield return type;
                         }
                     }
        }
    }
}