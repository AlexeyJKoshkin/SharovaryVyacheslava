using System;
using System.Text;

namespace CodeGenerator
{
    /// <summary>
    ///     Интерфейс класса который умеет генерировать код
    /// </summary>
    public interface ICodeGenerator
    {
        string Generate();
    }

    public abstract class BaseCodeGenerator : ICodeGenerator
    {
        private StringBuilder _stringBuilder;
        protected string NameSpace => CodeGeneratorConst.DefaultNameSpace;

        public string Generate()
        {
            var builder = new StringBuilder();
            builder.Append(GenerateUsingScope(GetUsings()));
            builder.Append(GenerateNameSpace());
            builder.Append("{\n");
            builder.Append(GenerateBodyScope());
            builder.Append("}\n");
            return builder.ToString();
        }

        protected abstract string[] GetUsings();

        protected abstract void BodyScope();

        private string GenerateNameSpace()
        {
            if (string.IsNullOrEmpty(NameSpace))
            {
                throw new ArgumentException("NameSpace is null or Emty");
            }

            return "namespace " + NameSpace + "\n";
        }

        private string GenerateUsingScope(string[] usings)
        {
            if (usings == null || usings.Length == 0)
            {
                return null;
            }

            var builder = new StringBuilder();
            foreach (var u in usings)
            {
                builder.Append("using ");
                builder.Append(u);
                builder.Append(";\n");
            }

            builder.Append("\n");
            return builder.ToString();
        }

        private string GenerateBodyScope()
        {
            _stringBuilder = new StringBuilder();
            BodyScope();
            var res = _stringBuilder.ToString();
            _stringBuilder = null;
            return res;
        }


        protected void Append(string appendValue)
        {
            _stringBuilder.Append(appendValue);
        }

        protected void AppendLine(string appendLine)
        {
            _stringBuilder.AppendLine(appendLine);
        }

        protected void AppendLine()
        {
            _stringBuilder.AppendLine("\n");
        }


        protected void BeginTabBracers(int tabCount = 1)
        {
            AppendTab(tabCount);
            _stringBuilder.AppendLine("{");
        }

        protected void AppendTab(int countTabs = 1)
        {
            for (int i = 0; i < countTabs; i++) _stringBuilder.Append("\t");
        }


        protected void EndTabBracers(int tabCount = 1)
        {
            AppendTab(tabCount);
            _stringBuilder.AppendLine("}");
        }
    }
}