using System.Collections.Generic;
using UnityEngine;

namespace Core.Installers
{
    public class GameRootUnityCallbackReceiver : MonoBehaviour, IRoyalAxePauseSystemSwitcher
    {
        private readonly List<Feature> _onUpdateFeature = new List<Feature>();
        private readonly List<Feature> _onPauseAbleUpdateFeature = new List<Feature>();

        private bool _isPause;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            DoUpdate(_onUpdateFeature);
            if (_isPause)   return;
           
            DoUpdate(_onPauseAbleUpdateFeature);
        }

        public void AddUpdate(Feature updateFeature)
        {
            if (updateFeature == null)
            {
                return;
            }
            _onUpdateFeature.Add(updateFeature);
            HandleAddFeature(updateFeature);
        }

        public void RemoveUpdate(Feature updateFeature)
        {
            if (updateFeature == null)
            {
                return;
            }

            
            _onUpdateFeature.Remove(updateFeature);
            _onPauseAbleUpdateFeature.Remove(updateFeature);
            HandleRemoveFeature(updateFeature);
            
        }

        public void AddPauseAbleUpdate(Feature pauseAble)
        {
            if (pauseAble == null)
            {
                return;
            }

            _onPauseAbleUpdateFeature.Add(pauseAble);
            HandleAddFeature(pauseAble);
        }

        private void DoUpdate(List<Feature> systems)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                var s = systems[i];
                s.Execute();
                s.Cleanup();
            }
        }

        private void HandleAddFeature(Feature feature)
        {
            feature.Initialize();
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
            feature.gameObject.transform.SetParent(transform);
#endif
        }
        
        private void HandleRemoveFeature(Feature feature)
        {
            feature.Cleanup();
            feature.ClearReactiveSystems();
            feature.DeactivateReactiveSystems();
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
            Destroy(feature.gameObject);
#endif
        }

        void IRoyalAxePauseSystemSwitcher.SetPause()
        {
            _isPause = true;
        }

        void IRoyalAxePauseSystemSwitcher.UnPause()
        {
            _isPause = false;
        }

        void IRoyalAxePauseSystemSwitcher.SetState(bool isPause)
        {
            _isPause = isPause;
        }
    }
}