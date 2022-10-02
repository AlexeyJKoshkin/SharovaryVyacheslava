using System;
using System.Collections.Generic;
using GameKit.Editor;

namespace CodeGenerator
{
    public class EnumExtensionGenerator : ClassGenerator
    {
        private readonly Type[] _generatedType;
        private readonly string _methodBlank = "public static {2} {1}(this {0} a, {0} b)";

        private readonly KeyValuePair<string, string>[] _methodsNameBody =
        {
            new KeyValuePair<string, string>("SetFlag", "return a | b;"),
            new KeyValuePair<string, string>("UnsetFlag", "return a & (~b);"),
            new KeyValuePair<string, string>("ToogleFlag", " return a ^ b;"),
            new KeyValuePair<string, string>("HasFlag", " return (a & b) == b;")
        };

        public EnumExtensionGenerator(Type[] enumTypesGenerate) : base("KitchenEnumExtension", true)
        {
            foreach (var t in enumTypesGenerate)
            {
                if (!t.IsEnum)
                {
                    throw new ArgumentException($"{t.Name} is not Enum");
                }

                if (!t.HasAttribute<FlagsAttribute>())
                {
                    throw new ArgumentException($"{t.Name} doesn't has FlagsAttribute");
                }
            }

            _generatedType = enumTypesGenerate;
        }

        protected override string[] GetUsings()
        {
            return new[] {"System", "System.Collections.Generic"};
        }

        protected override string GetSummary()
        {
            return "\t/// <summary>\n\t/// Extension for all enums with [Flags]\n\t/// </summary>\n";
        }

        protected override void AppendClassBodyScope()
        {
            foreach (var t in _generatedType)
            {
                AppendTab(2);
                AppendLine($"#region {t.FullName}\n");
                AppendMethodsForType(t);
                AppendIenumaration(t);
                AppendTab(2);
                AppendLine($"#endregion {t.FullName}\n");
            }
        }

        private void AppendIenumaration(Type type)
        {
            AppendTab(2);
            AppendLine($" public static IEnumerable<{type.FullName}> All(this {type.FullName} a)");
            BeginTabBracers();
            AppendTab(2);
            AppendLine($" foreach ({type.FullName} d in Enum.GetValues(typeof({type.FullName})))");
            AppendTab(3);
            AppendLine("   if (a.HasFlag(d)) yield return d;");
            EndTabBracers();
        }

        private void AppendMethodsForType(Type t)
        {
            for (var i = 0; i < _methodsNameBody.Length - 1; i++)
                AppendMethod(_methodsNameBody[i], t.Name, t.Name);
            AppendMethod(_methodsNameBody[3], t.Name, "bool");
        }

        private void AppendMethod(KeyValuePair<string, string> nameBody, string paramName,
                                  string returnType)
        {
            AppendTab(2);
            AppendLine(string.Format(_methodBlank, paramName, nameBody.Key, returnType));
            AppendTab();
            BeginTabBracers();
            AppendTab(3);
            AppendLine(nameBody.Value);
            AppendTab();
            EndTabBracers();
        }
    }
}