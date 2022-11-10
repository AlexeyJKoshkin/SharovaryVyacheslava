using System;
using System.Collections.Generic;
using RoyalAxe.CoreLevel;
using UnityEngine;

namespace RoyalAxe.Units
{
    public class WizardShopUnitView : BaseUnitView, IBound
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public Bounds Bounds => _spriteRenderer.bounds;
        
        public event Action<Collider2D> OnEnterTriggerEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnterTriggerEvent?.Invoke(other);
        }
        public override IEnumerable<IViewEntityBehaviour> EntityBehaviours()
        {
            yield break;
        }
    }
}
