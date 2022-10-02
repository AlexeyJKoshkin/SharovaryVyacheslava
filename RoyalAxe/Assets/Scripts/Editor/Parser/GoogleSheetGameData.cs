using System.Collections.Generic;
using Core.Parser;

namespace Core.EditorCore.Parser
{
    public class GoogleSheetGameData
    {
        public string PageName;
        public string SheetName;
        public string[] RowNames;
        //клетки гугл таблицы
        public List<List<ICellValue>> Cells;
    }
}