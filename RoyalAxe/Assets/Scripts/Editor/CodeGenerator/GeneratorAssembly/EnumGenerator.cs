namespace CodeGenerator.EnumGenerator
{
    /// <summary>
    ///     Позволяет догенерировать Значения Enum
    /// </summary>
    public class EnumGenerator : BaseCodeGenerator
    {
        private readonly IEnumMetaData _metaData;

        private readonly string[] _usings;

        public EnumGenerator(IEnumMetaData metaData)
        {
            _metaData = metaData;
            if (metaData.IsFlag)
            {
                _usings = new[] {"System"};
            }
        }

        protected override string[] GetUsings()
        {
            return _usings;
        }

        protected override void BodyScope()
        {
            AppendLine(GetEnumAttribute());
            AppendLine("\tpublic enum " + _metaData.NameType);
            BeginTabBracers();
            foreach (var meta in _metaData)
                AddEnumRecord(meta);

            EndTabBracers();
            AppendLine();
        }

        private void AddEnumRecord(IEnumValueMeta meta)
        {
            TryAddAttributeLine(meta.Attribute);
            AppendLine($"\t\t{meta.EnumName} = {meta.IntValue},");
            Append("\n");
        }


        private void TryAddAttributeLine(string attributeString)
        {
            if (string.IsNullOrEmpty(attributeString))
            {
                return;
            }

            AppendLine(attributeString);
        }

        private string GetEnumAttribute()
        {
            var flags = _metaData.IsFlag ? ",Flags" : null;
            return $"\t[GameKit.CanGenerate({_metaData.CanGenerateTrue.ToString().ToLower()}) {flags}]";
        }
    }
}