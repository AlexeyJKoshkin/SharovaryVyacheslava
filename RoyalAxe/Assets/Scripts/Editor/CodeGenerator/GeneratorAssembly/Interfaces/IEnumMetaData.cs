using System.Collections.Generic;

namespace CodeGenerator.EnumGenerator
{
    public interface IEnumData
    {
        string NameType { get; }
        bool IsFlag { get; }
        bool CanGenerateTrue { get; }
    }

    /// <summary>
    ///     Храним данные по енуму
    /// </summary>
    public interface IEnumMetaData : IEnumData, IEnumerable<IEnumValueMeta>
    {
        int Count { get; }

        IEnumValueMeta GetEnumMeta(int i);
    }
}