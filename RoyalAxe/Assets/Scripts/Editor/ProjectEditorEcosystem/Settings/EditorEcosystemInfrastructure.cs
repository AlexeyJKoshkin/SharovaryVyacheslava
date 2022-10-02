#region

using GameKit.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace ProjectEditorEcoSystem
{
    public class EditorEcosystemInfrastructure : ScriptableObject
    {
        public EditorDataPathSettings Path => _projectSettings;
        [SerializeField] private ProjectEditorEcosystemLauncher _launcher;
        [SerializeField] private EditorDataPathSettings _projectSettings;

        [Button]
        private void Create()
        {
            OnCreate();
        }

#if UNITY_EDITOR
        private void OnCreate()
        {
            _projectSettings = EditorDataPathSettings.Instance;
            _launcher        = CheckObject(_launcher);
        }

        private T CheckObject<T>(T launcher) where T : ScriptableObject
        {
            if (launcher == null)
            {
                return EditorUtils.CreateAsset<T>(_projectSettings.DataFolderPath + $"/{typeof(T).Name}", true);
            }

            return launcher;
        }
#else
        private void OnCreate()
        {
         
        }
#endif
    }
}