using FluentBehaviourTree;


namespace Core.Launcher
{
    public class MetaSceneState : RoyalAxeSceneState<IMetaSceneInfrastructure>, IMetaSceneContext
    {
        private readonly TempMetaStateDirector _director;
        
        public MetaSceneState(IMetaSceneInfrastructure metaSceneInfrastructure,TempMetaStateDirector director) : base(metaSceneInfrastructure)
        {
            _director = director;
        }

        protected override IBehaviourTreeNode GetBehavior()
        {
            _director.Initialize();
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