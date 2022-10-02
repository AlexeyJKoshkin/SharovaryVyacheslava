using System.Collections.Generic;
using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public abstract class RAReactiveSystem<TEntity> : ReactiveSystem<TEntity> where TEntity : class, IEntity
    {
        protected RAReactiveSystem(IContext<TEntity> context) : base(context) { }
        protected RAReactiveSystem(ICollector<TEntity> collector) : base(collector) { }

        protected sealed override void Execute(List<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++) Execute(entities[i]);
        }

        protected abstract void Execute(TEntity e);
    }
}