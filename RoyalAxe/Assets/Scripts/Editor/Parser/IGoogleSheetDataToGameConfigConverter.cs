using System.Collections.Generic;

namespace Core.EditorCore.Parser
{
    public interface IGoogleSheetDataToGameConfigConverter
    {
        void ParseSheetData(IEnumerable<GoogleSheetGameData> data);
    }
}