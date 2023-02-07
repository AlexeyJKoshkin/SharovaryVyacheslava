using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.Data.Provider;
using Core.EditorCore.Parser;
using Core.Parser;


namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    public interface IConfigUpdater
    {
        void UpdateConfigs(List<GoogleSheetGameData> allPages,
                           IJsonConfigModelsOperation operation,
                           IGameDataParser parser);
    }
    

    internal abstract class ModelsToJsonFile<T> : IConfigUpdater where T : class,IDataObject,  new()
    {
        private List<T> _allExistItems;
     
        public void UpdateConfigs(List<GoogleSheetGameData> allPages,
                                  IJsonConfigModelsOperation operation,  IGameDataParser parser)
        {
            _allExistItems = operation.Load<T>().ToList();
            RemoveUpdateConfigs(_allExistItems, allPages);


            allPages.ForEach(p => //распарсили каждую страничку и добавили их в общий список
                             {
                                 var newItemsFromPage = Parse(p, parser);
                                 _allExistItems.AddRange(newItemsFromPage);
                             });
            
            operation.Save(_allExistItems);
        }

        protected virtual void RemoveUpdateConfigs(List<T> allExistItems, List<GoogleSheetGameData> allPages)
        {
            allExistItems.RemoveAll(o => allPages.Any(p => p.PageName == o.UniqueID)); //удаляем все модели которые будем обновлять
        }

        protected abstract IEnumerable<T> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser);

        protected void AddToListData<TSomeData>(List<TSomeData> list, IGameDataParser parser, List<ICellValue> cells) where TSomeData : new()
        {
            var result = new TSomeData();
            parser.UpdateObject(cells, result);
            list.Add(result);
        }
    }
}
