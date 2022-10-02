using Sirenix.OdinInspector;

namespace ProjectEditorEcoSystem
{
    public class EditorDataPathSettings : SingletonSettings<EditorDataPathSettings>
    {
        [FolderPath] public string DataFolderPath = "Assets/SharovaruVaycheslava/";

        private string BuildPath(string folder)
        {
            return DataFolderPath + "/" + folder;
        }
    }
}