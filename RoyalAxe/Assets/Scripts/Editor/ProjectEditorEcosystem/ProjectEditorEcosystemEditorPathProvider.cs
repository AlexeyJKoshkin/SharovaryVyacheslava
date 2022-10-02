using Core.Data.Provider;
using UnityEngine;

namespace ProjectEditorEcoSystem
{
    public class ProjectEditorEcosystemEditorPathProvider : IProjectEditorEcosystemEditorPathProvider
    {
        private readonly EditorDataPathSettings _projectPathSettings;
        private readonly IDataStorage _storage;

        public ProjectEditorEcosystemEditorPathProvider(IDataStorage storage, EditorDataPathSettings projectPathSettings)
        {
            _storage             = storage;
            _projectPathSettings = projectPathSettings;
        }

        public void CreateInfrastructure()
        {
            CreateFolders();
        }

        private void CreateFolders()
        {
            Debug.LogError("Create folders");
            /*foreach (var part in _storage.All<GameBoxDef>())
            {
                var folderPath = _projectPathSettings.DataPath.GetPartGameFolderPath(part);
                part.FolderPath = folderPath;
                EditorUtils.CreateAssetFolder(folderPath);
                foreach (var cardType in _storage.All<CardTypeSO>())
                {
                    folderPath = $"{folderPath}/{cardType.UniqueID}";
                    EditorUtils.CreateAssetFolder(folderPath);
                }
            }*/
        }
    }
}