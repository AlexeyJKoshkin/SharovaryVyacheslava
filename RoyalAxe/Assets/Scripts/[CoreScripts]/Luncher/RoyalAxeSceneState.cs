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
            OnEnterState();
        }

        BehaviourTreeStatus IBehaviourTreeNode.Execute(TimeData data)
        {
            OnExecute(data);
            return _result;
        }
        
        protected virtual void OnExitState() { }

        // к этому времени все ресурсы загружены, сцена полностью готова к работе.
        //основная логика иниациализации сцены.
        //обновление данных
        protected virtual void OnEnterState() { }
        
        protected void LoadScene(ISceneLoaderHelper sceneLoaderHelper)
        {
            Successfully();
            SceneLoader.LoadScene(sceneLoaderHelper);
        }
        
        protected virtual void OnExecute(TimeData data)
        {
            Continue();
        }
        
        protected void Successfully()
        {
            Context.mainLoopStateEntity.RemoveMainLoopState();
            _result = BehaviourTreeStatus.Success;
        }

        protected  void Fail()
        {
            _result = BehaviourTreeStatus.Failure;
        }

        protected   void Continue()
        {
            _result = BehaviourTreeStatus.Running;
        }
        
     


    }
}
