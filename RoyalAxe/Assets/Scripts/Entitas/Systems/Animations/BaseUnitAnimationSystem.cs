using System;
using System.Collections.Generic;
using Core;
using Entitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public abstract class BaseUnitAnimationSystem : IInitializeSystem, ITearDownSystem, ICleanupSystem, IReactiveSystem
    {
        protected readonly IContext<RAAnimationEntity> Context;

        protected readonly List<TriggerUnitHandlerSystem> Bindings = new List<TriggerUnitHandlerSystem>();

        protected void AddSubSystem(IMatcher<RAAnimationEntity> matcher, Action<RAAnimationEntity, Animator> animationAction, GroupEvent groupEvent = GroupEvent.Added)
        {
            var collector = Context.CreateCollector(new TriggerOnEvent<RAAnimationEntity>(matcher, groupEvent));
            Bindings.Add(new TriggerUnitHandlerSystem(collector, animationAction));
        }

        public BaseUnitAnimationSystem(IContext<RAAnimationEntity> context)
        {
            Context = context;
        }

        public abstract void Initialize();

        public virtual void TearDown()
        {
            HLogger.LogError("TearDown");
        }

        public void Cleanup()
        {
          //  HLogger.LogError("Cleanup");
        }

        public void Execute()
        {
            Bindings.ForEach(e => e.Execute());
        }

        public void Activate()
        {
            Bindings.ForEach(e => e.Activate());
        }

        public void Deactivate()
        {
            Bindings.ForEach(e => e.Deactivate());
        }

        public void Clear()
        {
            Bindings.ForEach(e => e.Clear());
        }

        protected IMatcher<RAAnimationEntity> GetWith(IMatcher<RAAnimationEntity> with)
        {
            return Matcher<RAAnimationEntity>.AllOf(RAAnimationMatcher.Animator, with);
        }
    }
}