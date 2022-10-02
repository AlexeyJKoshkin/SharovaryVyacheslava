#region

using Core.Data.Provider;
using GameKit.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

#endregion

namespace Core.EditorCore
{
    [CustomEditor(typeof(DataBox), true)]
    public class BaseDataBoxInspectorEditor : OdinEditor
    {
        private SubConfigsEditorFacade<ScriptableObject> _defenitions;

        private bool AddFromFolder;

        protected override void OnEnable()
        {
            base.OnEnable();
            /*EditorUtils.FixMissingScript(target);
            ISubScriptableFactory factory = new DefaultSubScriptableFactory(((DataBox) target).ObjectType);
            _defenitions = new SubConfigsEditorFacade<ScriptableObject>(serializedObject, serializedObject.FindProperty("_collection"), "Definition", factory);*/
        }
    }
}