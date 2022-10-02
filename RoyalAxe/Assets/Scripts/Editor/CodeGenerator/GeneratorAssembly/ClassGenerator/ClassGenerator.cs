namespace CodeGenerator
{
    public class ClassGenerator : BaseCodeGenerator
    {
        private readonly string _className;
        private readonly bool _isStatic;

        public ClassGenerator(string name, bool isStatic = false)
        {
            _isStatic  = isStatic;
            _className = name;
        }

        protected override string[] GetUsings()
        {
            return null;
        }


        protected sealed override void BodyScope()
        {
            Append(GetSummary());
            AppendLine(GetClassHeader(_className, _isStatic, GetInterfaces()));
            BeginTabBracers();
            AppendClassBodyScope();
            EndTabBracers();
        }

        protected virtual string[] GetInterfaces()
        {
            return null;
        }


        protected virtual string GetSummary()
        {
            return null;
        }

        protected virtual void AppendClassBodyScope() { }


        private static string GetClassHeader(string className, bool isStatic, string[] interfaces)
        {
            var    st     = isStatic ? "static" : null;
            string result = $"\tpublic {st} class {className}";

            if (!isStatic && interfaces != null && interfaces.Length > 0)
            {
                result += $" : {interfaces[0]}";
                for (int i = 1; i < interfaces.Length; i++) result += $", {interfaces[i]}";
            }

            return result;
        }
    }
}