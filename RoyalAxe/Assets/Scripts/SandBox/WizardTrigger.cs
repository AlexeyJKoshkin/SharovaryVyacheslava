using System;
using UnityEngine;

namespace RoyalAxe.CoreLevel {
    public class WizardTrigger : MonoBehaviour
    {
        public event Action<Collider2D> OnEnterTriggerEvent;


        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnterTriggerEvent?.Invoke(other);
        }
    }
}