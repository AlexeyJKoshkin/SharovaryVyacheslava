using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Core;
using GameKit.Editor;

namespace CodeGenerator.EnumGenerator
{
    public class EnumParsedData : IEnumData
    {
        public EnumParsedData(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException($"{enumType.Name} is not Enum");
            }

            EnumType = enumType;

            IsFlag = enumType.HasAttribute<FlagsAttribute>();

            var attr = enumType.GetCustomAttribute<CanGenerateAttribute>();
            CanGenerateTrue = attr?.CanRemoveValue ?? false;
        }

        protected Type EnumType { get; }

        public string NameType => EnumType == null ? null : EnumType.Name;

        public bool IsFlag { get; }
        public bool CanGenerateTrue { get; }
    }


    /// <summary>
    ///     Парсит файл енума. Вычленяет строковые значения и комментарий
    /// </summary>
    public class OldEnumFileParser : EnumParsedData, IEnumMetaData
    {
        private readonly EnumValueMeta[] _valuesMeta;

        public OldEnumFileParser(Type enumType) : base(enumType)
        {
            _valuesMeta = Enum.GetValues(enumType).Cast<Enum>().Select(e => new EnumValueMeta(e)).ToArray();


            for (var i = 0; i < _valuesMeta.Length; i++)
            {
                var oldAttributesData = GetOldAttributesData(i);
                _valuesMeta[i].Attribute = GetAttributeString(oldAttributesData);
            }
        }

        public int Count => _valuesMeta.Length;

        public IEnumValueMeta GetEnumMeta(int i)
        {
            return _valuesMeta[i];
        }

        public IEnumerator<IEnumValueMeta> GetEnumerator()
        {
            return _valuesMeta.Cast<IEnumValueMeta>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private List<CustomAttributeData> GetOldAttributesData(int indexEnum)
        {
            var sortedList = new List<CustomAttributeData>();

            var memInfo = EnumType.GetMember(_valuesMeta[indexEnum].EnumName);
            if (memInfo.Length == 0)
            {
                return sortedList;
            }

            var l = memInfo[0].GetCustomAttributesData();
            if (l.Count == 0)
            {
                return sortedList;
            }

            sortedList.AddRange(l);
            sortedList.Sort(new AttributesComparer());
            return sortedList;
        }

        private string GetAttributeString(List<CustomAttributeData> list)
        {
            var builder = new StringBuilder();
            for (var j = 0; j < list.Count; j++)
            {
                AddAttibute(list[j], builder);
                if (j < list.Count - 1)
                {
                    builder.Append("\n");
                }
            }

            return builder.ToString();
        }

        private void AddAttibute(CustomAttributeData customAttributeData, StringBuilder builder)
        {
            builder.Append("\t\t[");
            builder.Append(customAttributeData.AttributeType.FullName);
            var hasArguments = customAttributeData.ConstructorArguments.Count > 0;

            if (hasArguments)
            {
                builder.Append("(");
            }

            for (var i = 0; i < customAttributeData.ConstructorArguments.Count; i++)
            {
                var arg = customAttributeData.ConstructorArguments[i];
                AddAttibuteArgument(arg, builder);

                if (i < customAttributeData.ConstructorArguments.Count - 1)
                {
                    builder.Append(",");
                }
            }

            if (hasArguments)
            {
                builder.Append(")");
            }

            builder.Append("]");
        }

        private void AddAttibuteArgument(CustomAttributeTypedArgument arg, StringBuilder builder)
        {
            var t = ArgumentType.Other;

            if (arg.ArgumentType == typeof(string))
            {
                t = ArgumentType.String;
            }
            else if (arg.ArgumentType.IsSubclassOf(typeof(Enum)))
            {
                t = ArgumentType.Enum;
            }

            if (t == ArgumentType.String)
            {
                builder.Append("\"");
            }

            if (t == ArgumentType.Enum)
            {
                builder.Append(arg.ArgumentType.FullName);
                builder.Append(".");
            }

            builder.Append(arg.Value);

            if (t == ArgumentType.String)
            {
                builder.Append("\"");
            }
        }

        private class AttributesComparer : IComparer<CustomAttributeData>
        {
            public int Compare(CustomAttributeData x, CustomAttributeData y)
            {
                return x.AttributeType.Name.CompareTo(y.AttributeType.Name);
            }
        }

        private enum ArgumentType
        {
            String,
            Enum,
            Other
        }
    }
}