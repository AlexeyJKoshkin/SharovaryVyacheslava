using System;
using System.Collections.Generic;
using Core.Configs;
using UnityEngine;

namespace ProjectEditorEcoSystem
{
    public class ProjectProjectEditorEcosystem : IProjectEditorEcosystem, IProjectEditorUtility
    {
        private readonly IJsonConfigModelsOperation _jsonConfigModelsOperation;
        private readonly ProjectEditorEcoSystemGuiDrawer _gui;

        public ProjectProjectEditorEcosystem(IReadOnlyList<IProjectEditorEcosystemGUI> gui, IJsonConfigModelsOperation jsonConfigModelsOperation)
        {
            _jsonConfigModelsOperation = jsonConfigModelsOperation;
            _gui                       = new ProjectEditorEcoSystemGuiDrawer(gui, this);
        }

        public event Action OnFinishWorkingEvent;


        public void StartWork()
        {
            Debug.LogError("Start Editor Eco System");
        }

        public void StopWork()
        {
            Debug.LogError("StopWork World");
            OnFinishWorkingEvent?.Invoke();
        }

        public void DrawGUI()
        {
            _gui.DrawGUI();
        }

        public IProjectEditorUtility Utility => this;
        IJsonConfigModelsOperation IProjectEditorUtility.ConfigOperation => _jsonConfigModelsOperation;
    }
}