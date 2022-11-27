using DG.Tweening;
using FluentBehaviourTree;
using UnityEngine;

namespace Core.Launcher
{
    public abstract class RoyalAxeSceneState<T> : IProjectSceneState where T : IStateInfrastructure
    {
        protected T Infrastructure => _stateInfrastructure;
        readonly T _stateInfrastructure;
        public virtual string NodeName => this.GetType().Name;
        private GameRootLoopContext Context => _stateInfrastructure.Contexts.gameRootLoop;
        private ISceneLoader SceneLoader => _stateInfrastructure.SceneLoader;
        private BehaviourTreeStatus _result = BehaviourTreeStatus.Running;
        private IBehaviourTreeNode _behaviour;

        public RoyalAxeSceneState(T stateInfrastructure)
        {
            _stateInfrastructure = stateInfrastructure;
        }

        void IFMSState.ExitState()
        {
            OnExitState();
        }

        void IFMSState.EnterState()
        {
            _behaviour = GetBehavior();
        }

        BehaviourTreeStatus IBehaviourTreeNode.Execute(TimeData data)
        {
            OnExecute(data);
            return _result;
        }

        protected abstract IBehaviourTreeNode GetBehavior();

        // к этому времени все ресурсы загружены, сцена полностью готова к работе.
        //основная логика иниациализации сцены.
        //обновление данных
        protected virtual void OnExitState() { }

        protected void LoadScene(ISceneLoaderHelper sceneLoaderHelper)
        {
            Successfully();
            SceneLoader.LoadScene(sceneLoaderHelper);
        }

        protected virtual void OnExecute(TimeData data)
        {
            CheckCanFinish(data);
        }

        protected void CheckCanFinish(TimeData delta)
        {
            if (_behaviour == null)
            {
                Fail();
            }
            else
            {
                _behaviour.Execute(delta);
                var sceneLoader = _stateInfrastructure.StateLoaderProvider.GetCurrentSceneLoader(); // просто проверяем что можно грузить следующую сцену
                if (sceneLoader == null) Continue();
                else LoadScene(sceneLoader);
            }
        }


        protected void Successfully()
        {
            Context.mainLoopStateEntity.RemoveMainLoopState();
            _result = BehaviourTreeStatus.Success;
        }

        protected void Fail()
        {
            _result = BehaviourTreeStatus.Failure;
        }

        protected void Continue()
        {
            _result = BehaviourTreeStatus.Running;
        }
    }
}