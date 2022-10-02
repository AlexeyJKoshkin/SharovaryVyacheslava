namespace CodeGenerator.EnumGenerator
{
    /// <summary>
    ///     Данные по конкретному значению енума
    /// </summary>
    public interface IEnumValueMeta
    {
        string Attribute { get; }
        string EnumName { get; set; }
        int IntValue { get; }
    }
}