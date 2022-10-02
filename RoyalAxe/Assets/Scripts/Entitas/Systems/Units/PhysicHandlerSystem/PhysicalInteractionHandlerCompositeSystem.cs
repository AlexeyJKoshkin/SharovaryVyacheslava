using System;
using System.Collections.Generic;
using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public abstract class PhysicalInteractionHandlerCompositeSystem : IExecuteSystem, IReactiveSystem
    {
        protected UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        private readonly List<TriggerPhysicalInteraction> _physicalInteractionHandlerSystems = new List<TriggerPhysicalInteraction>();

        public PhysicalInteractionHandlerCompositeSystem(UnitsContext context)
        {
            _unitsContext = context;
        }

        protected void Add(UnitsMatcherLibrary.IMatcherBuilder matcherBuilder, Action<UnitsEntity> handler)
        {
            
            var collector = _unitsContext.CreateCollector(matcherBuilder.Build());
            _physicalInteractionHandlerSystems.Add(new TriggerPhysicalInteraction(handler, collector));
        }

        public void Execute()
        {
            Do(e => e.Execute());
        }
        public void Activate()
        {
            Do(e => e.Activate());
        }

        public void Deactivate()
        {
            Do(e => e.Deactivate());
        }

        public void Clear()
        {
            Do(e => e.Clear());
        }

        private void Do(Action<TriggerPhysicalInteraction> action)
        {
            for (int i = 0; i < _physicalInteractionHandlerSystems.Count; i++) action(_physicalInteractionHandlerSystems[i]);
        }
    }
}
