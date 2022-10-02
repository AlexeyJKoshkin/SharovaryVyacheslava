using Entitas;

namespace RoyalAxe.GameEntitas
{
    public abstract class AbstractEntityFactory<T, TContext> where TContext : IContext<T> where T : class, IEntity
    {
        protected TContext Context { get; }

        public AbstractEntityFactory(TContext context)
        {
            Context = context;
        }
    }
}