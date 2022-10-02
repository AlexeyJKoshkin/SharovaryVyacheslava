namespace Core
{
    public class ProjectRootContext : ContextScope
    {
        private void OnValidate()
        {
            IsRoot = true;
        }
    }
}