using System.Collections.Generic;

namespace Core.Parser
{
    public interface IGameDataParser
    {
        object UpdateObject(List<ICellValue> cells, object data);
        object GetValue(string columnName, string cellValue);
        bool UpdateObjectValue(object data, string columnName, string cellValue);
        bool CanRead(string rowName);
    }
    
    
}