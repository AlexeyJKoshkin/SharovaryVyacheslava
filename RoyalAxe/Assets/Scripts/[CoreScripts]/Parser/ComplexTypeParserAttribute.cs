using System;

namespace Core.Parser
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ComplexTypeParserAttribute : Attribute
    {
        public Type ParseableType;

        public ComplexTypeParserAttribute(Type type)
        {
            ParseableType = type;
        }
    }
}