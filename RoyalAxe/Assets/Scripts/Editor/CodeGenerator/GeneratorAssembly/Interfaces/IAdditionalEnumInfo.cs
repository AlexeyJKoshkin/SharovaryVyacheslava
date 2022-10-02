namespace CodeGenerator.EnumGenerator
{
    /// <summary>
    ///     Дополнительная настройка для енума
    /// </summary>
    public interface IAdditionalEnumInfo
    {
        /// <summary>
        ///     Строка атрибутов
        /// </summary>
        /// <returns></returns>
        string GetAttributeString();

        /// <summary>
        ///     Строка иимени занчения енума
        /// </summary>
        /// <param name="candidateText"></param>
        /// <returns></returns>
        string GetEnumName(string candidateText);

        /// <summary>
        ///     Получить int значение енума
        /// </summary>
        /// <returns></returns>
        int? GetIntValue();

        /// <summary>
        ///     ОТрисовать доп настройки
        /// </summary>
        void DoGUI();

        bool CanShow(IEnumValueMeta meta);
    }
}