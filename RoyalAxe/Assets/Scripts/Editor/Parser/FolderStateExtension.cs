using System;
using System.Collections.Generic;

namespace Core.EditorCore.Parser
{
    internal static class FolderStateExtension
    {
        public static FolderState SetFlag(this FolderState a, FolderState b)
        {
            return a | b;
        }

        public static FolderState UnsetFlag(this FolderState a, FolderState b)
        {
            return a & ~b;
        }

        public static FolderState ToggleFlag(this FolderState a, FolderState b)
        {
            return a ^ b;
        }

        public static bool HasFlag(this FolderState a, FolderState b)
        {
            return (a & b) == b;
        }

        public static IEnumerable<FolderState> All(this FolderState a)
        {
            foreach (FolderState d in Enum.GetValues(typeof(FolderState)))
                if (a.HasFlag(d))
                {
                    yield return d;
                }
        }
    }
}