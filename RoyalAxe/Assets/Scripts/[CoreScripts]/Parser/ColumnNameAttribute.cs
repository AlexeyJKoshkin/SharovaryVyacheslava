using System;

namespace Core.Parser
{
    /// <summary>
    ///     В моделях указываем имя колонки в гуглтаблицах
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ColumnNameAttribute : Attribute
    {
        public string ColumnName;

        public ColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
    
    
    /// <summary>
    ///     В моделях указываем имя колонки в гуглтаблицах
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class GoogleSheetPageNameAttribute : Attribute
    {
        public FieldNameType FieldName;
        public string PageName;

        public GoogleSheetPageNameAttribute(string columnName)
        {
            PageName = columnName;
            FieldName = string.IsNullOrEmpty(columnName) ? FieldNameType.SameAsFieldName : FieldNameType.Custom;
        }
        public GoogleSheetPageNameAttribute(FieldNameType type = FieldNameType.SameAsTypeName)
        {
            FieldName = type == FieldNameType.Custom ? FieldNameType.SameAsFieldName : type;
        }
        
        public enum FieldNameType
        {
            SameAsFieldName,
            Custom,
            SameAsTypeName
        }
    }
    
}
