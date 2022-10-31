using System;
using UnityEngine;

namespace RoyalAxe.CoreLevel 
{
    public class WizardTrigger : MonoBehaviour, IBound
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public Bounds Bounds => _spriteRenderer.bounds;
        
        public event Action<Collider2D> OnEnterTriggerEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnterTriggerEvent?.Invoke(other);
        }
    }
}