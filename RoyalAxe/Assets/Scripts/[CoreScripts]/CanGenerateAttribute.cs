using System;

namespace Core
{
    /// <summary>
    ///     Этим атрибутом маркировать, то что можно генерировать или догенерировать
    /// </summary>
    public class CanGenerateAttribute : Attribute
    {
        public CanGenerateAttribute()
        {
            CanRemoveValue = false;
        }

        public CanGenerateAttribute(bool canRemove)
        {
            CanRemoveValue = canRemove;
        }

        /// <summary>
        ///     Можно ли удалять значение енума в окне генератора енума
        /// </summary>
        public bool CanRemoveValue { get; }
    }
}