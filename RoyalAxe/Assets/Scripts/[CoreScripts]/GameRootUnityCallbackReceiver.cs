using System.Collections.Generic;
using UnityEngine;

namespace Core.Installers
{
    public class RoyalAxePauseSystemSwitcher : IRoyalAxePauseSystemSwitcher
    {
        private readonly IRoyalAxePauseSystemSwitcher _receiver;
        private readonly GameRootLoopContext _context;
        public RoyalAxePauseSystemSwitcher(GameRootUnityCallbackReceiver receiver, GameRootLoopContext context)
        {
            _receiver = receiver;
            _context = context;
            _context.CreateEntity().AddGamePause(false);
        }

        public void SetPause()
        {
            _context.gamePauseEntity.ReplaceGamePause(true);
            _receiver.SetPause();
        }

        public void UnPause()
        {
            _context.gamePauseEntity.ReplaceGamePause(false);
            _receiver.UnPause();
        }

        public void SetState(bool isPause)
        {
            if (_context.gamePause.IsPause == isPause) return;

            if(isPause)
                UnPause();
            else
                SetPause();
        }
    }

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
            if (_isPause)
            {
                return;
            }

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