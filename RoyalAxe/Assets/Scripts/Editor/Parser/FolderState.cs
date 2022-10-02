using System;

namespace Core.EditorCore.Parser
{
    [Flags]
    public enum FolderState
    {
        None = 0,
        New = 1 << 1,
        Ignore = 1 << 2,
        Update = 1 << 3,
        Missed = 1 << 4
    }
}