using System.Linq;
using Core.Data.Provider;
using FluentBehaviourTree;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Core.Launcher
{
    /*public abstract class AbstractSceneState : IProjectSceneState
    {
        public virtual string NodeName => GetType().Name;
        
        public BehaviourTreeStatus Execute(TimeData time)
        {
            throw new System.NotImplementedException();
        }
        void IFMSState.ExitState()
        {
            OnExitState();
        }

        void IFMSState.EnterState()
        {
            OnEnterState();
        }

        protected virtual void OnExitState() { }

        // к этому времени все ресурсы загружены, сцена полностью готова к работе.
        //основная логика иниациализации сцены.
        //обновление данных
        protected virtual void OnEnterState() { }
        
        [SerializeField, GameKit.ReadOnly] private BehaviourTreeStatus _result = BehaviourTreeStatus.Running;

        BehaviourTreeStatus IBehaviourTreeNode.Execute(TimeData data)
        {
            OnExecute(data);
            return _result;
        }

        protected virtual void OnExecute(TimeData data)
        {
            Continue();
        }
    }*/

    /*
     * Состояние игры в какой либо сцене. Пока предлагаю использовать SOшники для быстрого тестирования и разработки модулей. На этапе сдачи проекта переделать в обычный класс
     */
    public abstract class AbstractSceneStateScriptable : ScriptableObject, IProjectSceneState
    {
        public virtual string NodeName => GetType().Name;

        [Inject] public IStateInfrastructure _stateInfrastructure;

    
        protected BehaviourTreeStatus Result => _result;

        public void ActivateScene(DataBox[] sceneData)
        {
            var builder = _stateInfrastructure.SystemsBuilder;
            
            var stateEntity = _stateInfrastructure.Context.CreateEntity();
            stateEntity.AddAdditionalDataBox(sceneData);
            
            var update = builder.GetAlwaysUpdateFeature().ToArray(); //  _sceneSystemBuilder.BuildUpdate($"{stateSettings.NodeName}_update");
            stateEntity.AddUpdateSystems(update);
            update = builder.GetPauseableUpdateFeature().ToArray(); //_sceneSystemBuilder.BuildPauseableUpdate($"{stateSettings.NodeName}_update_Pause");
            stateEntity.AddPauseableUpdateSystems(update);

            stateEntity.AddMainLoopState(this);
        }

        [Button]
        protected void LoadScene(GameSceneType meta)
        {
            Successfully();
            _stateInfrastructure.SceneLoader.LoadScene(meta);
        }

        void IFMSState.ExitState()
        {
            OnExitState();
        }

        void IFMSState.EnterState()
        {
            OnEnterState();
        }

        protected virtual void OnExitState() { }

        // к этому времени все ресурсы загружены, сцена полностью готова к работе.
        //основная логика иниациализации сцены.
        //обновление данных
        protected virtual void OnEnterState() { }

        [SerializeField, GameKit.ReadOnly] private BehaviourTreeStatus _result = BehaviourTreeStatus.Running;

        BehaviourTreeStatus IBehaviourTreeNode.Execute(TimeData data)
        {
            OnExecute(data);
            return _result;
        }

        protected virtual void OnExecute(TimeData data)
        {
            Continue();
        }

        [ButtonGroup("Set executeStat")]
        protected void Successfully()
        {
            _stateInfrastructure.Context.mainLoopStateEntity.RemoveMainLoopState();
            _result = BehaviourTreeStatus.Success;
        }

        [ButtonGroup("Set executeStat")]
        protected void Fail()
        {
            _result = BehaviourTreeStatus.Failure;
        }

        [ButtonGroup("Set executeStat")]
        protected void Continue()
        {
            _result = BehaviourTreeStatus.Running;
        }
    }
}