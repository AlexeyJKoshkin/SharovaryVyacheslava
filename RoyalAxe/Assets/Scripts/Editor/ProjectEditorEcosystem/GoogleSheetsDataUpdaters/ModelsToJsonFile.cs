using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.Data.Provider;
using Core.EditorCore.Parser;
using Core.Parser;


namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    public static class ParserExtensions
    {
        public static void AddParsedData<T>(this List<T> list, IGameDataParser parser, List<ICellValue> cells) where T : new()
        {
            var result = new T();
            parser.UpdateObject(cells, result);
            list.Add(result);
        }
    }

    public interface IConfigUpdater
    {
        void UpdateConfigs(List<GoogleSheetGameData> allPages,
                           IJsonConfigModelsOperation operation,
                           IGameDataParser parser);
    }


    public class ModelsToJsonHelper<T> where T : class, IDataObject
    {
        private readonly IJsonConfigModelsOperation _operation;

        private readonly Dictionary<string, T> _allExistItems;
        
        public ModelsToJsonHelper(IJsonConfigModelsOperation operation)
        {
            _operation = operation;
            _allExistItems = operation.Load<T>().ToDictionary(o => o.UniqueID, o => o);
        }

        public void UpdateModels(IEnumerable<T> newItems)
        {
            foreach (var item in newItems)
            {
                _allExistItems[item.UniqueID] = item;
            }
            _operation.Save(_allExistItems.Values.ToList());
        }
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
