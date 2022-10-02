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
}
