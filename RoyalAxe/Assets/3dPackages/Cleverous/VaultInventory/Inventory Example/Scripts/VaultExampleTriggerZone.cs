// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine;
using UnityEngine.Events;

namespace Cleverous.VaultInventory.Example
{
    public class VaultExampleTriggerZone : MonoBehaviour
    {
        public UnityEvent OnEnter;
        public UnityEvent OnSubmit;
        public UnityEvent OnExit;

        protected bool PlayerIsPresent;

        protected virtual void Update()
        {
            if (!PlayerIsPresent) return;
            if (VaultInventory.GetPressedInteract() && !VaultInventory.AnyBlockingUiMenuIsOpen) OnSubmit?.Invoke();
        }

        protected virtual void OnTriggerEnter(Collider col)
        {
            PlayerIsPresent = true;
            OnEnter?.Invoke();
        }
        protected virtual void OnTriggerExit(Collider col)
        {
            PlayerIsPresent = false;
            OnExit?.Invoke();
        }
    }
}