using System;
using System.Collections.Generic;
using Entitas;

namespace RoyalAxe.EntitasSystems {
    public class BaseMatcherBuilder<TEntity> : BaseMatcherBuilder<TEntity>.IMatcherBuilder where TEntity:class, IEntity
    {
        public interface IMatcherBuilder
        {
            TriggerOnEvent<TEntity> Build();

            IMatcherBuilder NoneOf(params IMatcher<TEntity>[] triggerDefineMather);
            
            IMatcherBuilder AnyOf(params IMatcher<TEntity>[] triggerDefineMather);

            TriggerOnEvent<TEntity> Added();
            TriggerOnEvent<TEntity> Removed();
            TriggerOnEvent<TEntity> AddedOrRemoved();
            IMatcher<TEntity> Matcher();
        }
        
        protected List<IMatcher<TEntity>> _defineMathchers;
        protected List<IMatcher<TEntity>> _noneOfMathchers;
        protected List<IMatcher<TEntity>> _anyOfMathchers;

        private GroupEvent _groupEvent = GroupEvent.Added;
        
        public IMatcher<TEntity> Matcher()
        {
            return Matcher<TEntity>.AllOf(_defineMathchers.ToArray()).NoneOf(_noneOfMathchers.ToArray());
        }
        
        public TriggerOnEvent<TEntity> Build()
        {
            return new TriggerOnEvent<TEntity>(Matcher(), _groupEvent);
        }

        IMatcherBuilder IMatcherBuilder.NoneOf(params IMatcher<TEntity>[] triggerNoneOfMatcher)
        {
            if (triggerNoneOfMatcher != null && triggerNoneOfMatcher.Length > 0)
                _noneOfMathchers.AddRange(triggerNoneOfMatcher);
            return this;
        }
        
       

        IMatcherBuilder IMatcherBuilder.AnyOf(params IMatcher<TEntity>[] triggerDefineMather)
        {
            throw new NotImplementedException();
        }

        public TriggerOnEvent<TEntity> Added()
        {
            return SetEvent(GroupEvent.Added);
        }

        public TriggerOnEvent<TEntity> Removed()
        {
            return SetEvent(GroupEvent.Removed);
        }

        public TriggerOnEvent<TEntity> AddedOrRemoved()
        {
            return SetEvent(GroupEvent.AddedOrRemoved);
        }

       

        TriggerOnEvent<TEntity> SetEvent(GroupEvent @event)
        {
            _groupEvent = @event;
            return Build();
        }
    }
}