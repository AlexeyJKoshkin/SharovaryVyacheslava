using System;

namespace CodeGenerator.EnumGenerator
{
    /// <summary>
    ///     Храним описание значения енума
    /// </summary>
    internal class EnumValueMeta : IEnumValueMeta
    {
        public EnumValueMeta(string name, int intval, string attribute)
        {
            EnumName  = name;
            IntValue  = intval;
            Attribute = attribute;
        }

        public EnumValueMeta(Enum e)
        {
            EnumName  = e.ToString();
            IntValue  = Convert.ToInt32(e);
            Attribute = null;
        }


        public string Attribute { get; set; }
        public string EnumName { get; set; }
        public int IntValue { get; private set; }
    }
}