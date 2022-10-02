using Core.Configs;
using GameKit.CustomGameEditor;

namespace ProjectEditorEcoSystem
{
    public interface IProjectEditorEcosystem : ICustomGameEditor
    {
        void DrawGUI();
        IProjectEditorUtility Utility { get; }
    }

    public interface IProjectEditorUtility
    {
        IJsonConfigModelsOperation ConfigOperation { get; }
    }
}