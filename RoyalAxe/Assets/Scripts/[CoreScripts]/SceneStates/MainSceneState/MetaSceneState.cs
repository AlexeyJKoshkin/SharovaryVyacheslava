using FluentBehaviourTree;


namespace Core.Launcher
{
    public class MetaSceneState : RoyalAxeSceneState<IMetaSceneInfrastructure>, IMetaSceneContext
    {

        private IStateLoaderProvider TempMetaStateDirector => Infrastructure.StateLoaderProvider;
        
        public MetaSceneState(IMetaSceneInfrastructure metaSceneInfrastructure) : base(metaSceneInfrastructure)
        {
        }

        protected override IBehaviourTreeNode GetBehavior()
        {
            TempMetaStateDirector.Initialize();
            return new ActionNode("Mock", (ts) => BehaviourTreeStatus.Running);
            
            /*var allStatesAsNode = _maineSceneStateProvider.AllStates().Cast<IBehaviourTreeNode>().ToArray();
            var builder         = new BehaviourTreeBuilder();
            // Каждый тик находим первое активное состояние и возвращаем его результат
            builder.Selector<SelectorWithMemory>("Обновляем первый попавшийся стейт", allStatesAsNode)
                   .End();
            return builder.Build();*/
           //_maineSceneStateProvider.GetState<MainStateMetaScene>().EnterState(); // первый стейт активируем принудительно*/
        }


      
       
    }
}