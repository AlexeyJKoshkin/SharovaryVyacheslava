using System.Linq;
using FluentBehaviourTree;
using UnityEngine;
using VContainer;

namespace Core.Launcher
{
    public class MetaSceneState : RoyalAxeSceneState<IMetaSceneInfrastructure>, IMetaSceneContext
    {
        /*[Inject] private IMaineSceneStateProvider _maineSceneStateProvider;*/
        private IBehaviourTreeNode _tree;

        private IStateLoaderProvider TempMetaStateDirector => Infrastructure.StateLoaderProvider;
        
        public MetaSceneState(IMetaSceneInfrastructure metaSceneInfrastructure) : base(metaSceneInfrastructure)
        {
        }

        /// <summary>
        ///     Каждый тик пробуем запускать все зарегестрированные запускаем все стейты из метасцены
        /// </summary>
        /// <param name="dt"></param>
        protected override void OnExecute(TimeData dt)
        {
            var sceneLoader = TempMetaStateDirector.GetCurrentSceneLoader();
            if(sceneLoader == null) Continue();
            else
            {
                LoadScene(sceneLoader);
            }
            /*switch (_tree.Execute(dt))
            {
                case BehaviourTreeStatus.Failure:
                    Debug.LogError("Что-то где-то пошло не так. Возможно тут стоит добавить отдельную ветку");
                    Fail();
                    break;
                case BehaviourTreeStatus.Running: //Стейт  работает
                    Continue();
                    break;
                case BehaviourTreeStatus.Success: //Стейт закончил свою работу. Пока считаем это окончанием работы всего стейты сцены"); 
                    Successfully();
                    break;
            }

            Continue();*/
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            TempMetaStateDirector.Initialize();
            /*_tree = CreateSelectorAllStates();
            _maineSceneStateProvider.GetState<MainStateMetaScene>().EnterState(); // первый стейт активируем принудительно*/
        }

        /*private IBehaviourTreeNode CreateSelectorAllStates()
        {
            var allStatesAsNode = _maineSceneStateProvider.AllStates().Cast<IBehaviourTreeNode>().ToArray();
            var builder         = new BehaviourTreeBuilder();
            // Каждый тик находим первое активное состояние и возвращаем его результат
            builder.Selector<SelectorWithMemory>("Обновляем первый попавшийся стейт", allStatesAsNode)
                   .End();
            return builder.Build();
        }*/
       
    }
}