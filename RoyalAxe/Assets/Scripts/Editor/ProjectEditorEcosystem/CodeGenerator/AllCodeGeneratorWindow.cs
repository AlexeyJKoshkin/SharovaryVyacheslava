#region

using System;
using System.Linq;
using GameKit.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

#endregion

namespace ProjectEditorEcoSystem
{
    public class AllCodeGeneratorWindow : EditorWindow
    {
        private readonly GridSelector<IProjectCodeGenerator> _selector;

        public AllCodeGeneratorWindow()
        {
            var codeGenerators = ReflectionHelper.GetAllTypesInSolutionWithInterface<IProjectCodeGenerator>()
                                                 .Select(Activator.CreateInstance)
                                                 .Cast<IProjectCodeGenerator>()
                                                 .ToArray();
            _selector = new GridSelector<IProjectCodeGenerator>(codeGenerators, GetContentForGenerator) {ItemDrawerCallback = DrawCodeGenerator};
        }

        public string TabName => "CodeGenerator";

        [MenuItem("Tools/Generation")]
        private static void ShowGeneratorWindow()
        {
            GetWindow<AllCodeGeneratorWindow>().Show();
        }


        private bool DrawCodeGenerator(IProjectCodeGenerator element, int index, GUIContent name, bool isselected)
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label(name);
                if (GUILayout.Button("Generate"))
                {
                    element.GenerateToFile();
                    AssetDatabase.Refresh();
                }
            }

            return false;
        }

        private GUIContent GetContentForGenerator(IProjectCodeGenerator value, int index)
        {
            return new GUIContent(value.FileType.Name);
        }

        private void OnGUI()
        {
            DrawTabGUI();
        }

        public void DrawTabGUI()
        {
            _selector.DoSelectGUI("All Generator");
            if (GUILayout.Button("Generate All"))
            {
                _selector.ForEach(e => e.Generate());
                AssetDatabase.Refresh();
            }
        }
    }
}