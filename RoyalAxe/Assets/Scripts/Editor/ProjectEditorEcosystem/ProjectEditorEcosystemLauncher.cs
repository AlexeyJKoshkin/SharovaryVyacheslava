using GameKit.CustomGameEditor;
using UnityEngine;

namespace ProjectEditorEcoSystem
{
    public class ProjectEditorEcosystemLauncher : GameEditorLauncher<ProjectProjectEditorEcosystem>
    {
        public IProjectEditorEcosystem Current
        {
            get
            {
                if (!IsWork)
                {
                    if (_forceLaunch)
                    {
                        Lunch();
                    }
                }

                return CustomGameEditor;
            }
        }
        [SerializeField] private bool _forceLaunch = true;
    }
}