#region

using System;
using Newtonsoft.Json.Serialization;

#endregion

namespace Core
{
    /// <summary>
    ///     Вспомогательная хрень для JSON, чтобы в Dictionary<TEnum, T> - Tenum - Хранился в int, а не в string
    /// </summary>
    public class EnumToIntKeyContractResolver : DefaultContractResolver
    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            var contract = base.CreateDictionaryContract(objectType);

            var keyType = contract.DictionaryKeyType;
            if (keyType.BaseType == typeof(Enum))
            {
                contract.DictionaryKeyResolver =
                    propName => ((int) Enum.Parse(keyType, propName)).ToString();
            }

            return contract;
        }
    }
}