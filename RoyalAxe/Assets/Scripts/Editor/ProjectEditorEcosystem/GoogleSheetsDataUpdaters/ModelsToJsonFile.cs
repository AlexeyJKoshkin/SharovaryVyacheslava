using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Core.Data.Provider;
using Core.EditorCore.Parser;
using Core.Parser;
using Sirenix.Utilities;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
   
    internal abstract class ModelsToJsonFile<T> where T : class,  new()
    {
        private List<T> _allExistItems;
        private readonly CompositeGenericParser _dataParser;

        public ModelsToJsonFile()
        {
            _dataParser = new CompositeGenericParser();
        }
        
        public CompositeGenericParser Bind<TType>()
        {
            return _dataParser.Bind<TType>();
        }
        public CompositeGenericParser Bind(Type type)
        {
            _dataParser.Bind(type);
            return _dataParser;
        }

        public void UpdateConfigs(List<GoogleSheetGameData> allPages, IJsonConfigModelsOperation operation)
        {
            _allExistItems = operation.Load<T>().ToList();
            RemoveUpdateConfigs(_allExistItems, allPages);


            allPages.ForEach(p => //распарсили каждую страничку и добавили их в общий список
                             {
                                 var newItemsFromPage = Parse(p, _dataParser);
                                 _allExistItems.AddRange(newItemsFromPage);
                             });
            
            operation.Save(_allExistItems);
        }

        protected abstract void RemoveUpdateConfigs(List<T> allExistItems, List<GoogleSheetGameData> allPages);

        protected abstract IEnumerable<T> Parse(GoogleSheetGameData googleSheetGameData, IGameDataParser dataParser);

        protected void AddToListData<TSomeData>(List<TSomeData> list, IGameDataParser parser, List<ICellValue> cells) where TSomeData : new()
        {
            var result = new TSomeData();
            parser.UpdateObject(cells, result);
            list.Add(result);
        }
    }
}
