using System;
using Entitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class TriggerUnitHandlerSystem : RAReactiveSystem<RAAnimationEntity>
    {
        private readonly Action<RAAnimationEntity, Animator> _doAnimation;

        public TriggerUnitHandlerSystem(ICollector<RAAnimationEntity> context, Action<RAAnimationEntity, Animator> doAnimation) : base(context)
        {
            _doAnimation = doAnimation;
        }

        protected override void Execute(RAAnimationEntity e)
        {
            _doAnimation(e, e.animator.Controller);
        }

        protected override ICollector<RAAnimationEntity> GetTrigger(IContext<RAAnimationEntity> context)
        {
            return null;
        }

        protected override bool Filter(RAAnimationEntity entity)
        {
            return entity.isEnabled;
        }
    }
}