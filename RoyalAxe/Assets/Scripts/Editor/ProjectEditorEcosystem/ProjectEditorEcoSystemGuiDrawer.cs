using System.Collections.Generic;
using GameKit.Editor;
using UnityEditor;
using UnityEngine;

namespace ProjectEditorEcoSystem
{
    public class ProjectEditorEcoSystemGuiDrawer
    {
        private readonly ProjectProjectEditorEcosystem _projectProjectEditorEcosystem;
        private GridSelector<IProjectEditorEcosystemGUI> _TabSelector;

        public ProjectEditorEcoSystemGuiDrawer(IReadOnlyList<IProjectEditorEcosystemGUI> gui, ProjectProjectEditorEcosystem projectProjectEditorEcosystem)
        {
            _projectProjectEditorEcosystem = projectProjectEditorEcosystem;
            _TabSelector = new GridSelector<IProjectEditorEcosystemGUI>(false, (value, index) => new GUIContent(value.TabName), 0)
            {
                ItemDrawerCallback = ItemDrawerCallback
            };
            _TabSelector.InitValues(gui);
        }

        private bool ItemDrawerCallback(IProjectEditorEcosystemGUI element, int index, GUIContent name, bool isselected)
        {
            GUIStyle style = EditorStyles.miniButton;
            if (index == 0)
            {
                style = EditorStyles.miniButtonLeft;
            }

            if (index == _TabSelector.Count - 1)
            {
                style = EditorStyles.miniButtonRight;
            }

            return GUILayout.Toggle(isselected, name, style);
        }


        public void DrawGUI()
        {
            _TabSelector.DoSelectGUI();
            _TabSelector.CurrentValue?.DrawTabGUI();
        }
    }
}