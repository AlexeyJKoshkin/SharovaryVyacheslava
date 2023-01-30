// (c) Copyright Cleverous 2023. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A class to manage interactions with <see cref="InteractableSceneObject"/> components in the scene. Required for interactions.
    /// </summary>
    public class InteractableSelector : MonoBehaviour
    {
        public int InteractRadius;
        public Inventory HostInventory;
        public LayerMask InteractableLayer;
        protected List<InteractableSceneObject> Targets;
        protected List<InteractableSceneObject> OutOfRange;
        protected bool InteractPressed;
        protected List<int> Deselect;

        protected virtual void Reset()
        {
            InteractRadius = 2;
            InteractableLayer = 1;
            HostInventory = null;
            Targets = null;
        }
        protected virtual void Awake()
        {
            Targets = new List<InteractableSceneObject>();
            Deselect = new List<int>();
        }
        protected virtual void Update()
        { 
            if (!InteractPressed) InteractPressed = VaultInventory.GetPressedInteract();
        }
        protected virtual void FixedUpdate()
        {
            Scan();
            if (InteractPressed) InteractWithCurrentTarget();
            InteractPressed = false;
        }

        /// <summary>
        /// Scan the radius around this interactor for valid interactable objects.
        /// </summary>
        protected virtual void Scan()
        {
            Targets.RemoveAll(x => x == null);

            RaycastHit[] scan = Physics.SphereCastAll(transform.position, InteractRadius, Vector3.up, 0.01f, InteractableLayer);
            foreach (RaycastHit hit in scan)
            {
                InteractableSceneObject comp = hit.transform.GetComponent<InteractableSceneObject>();
                if (comp != null && !Targets.Contains(comp)) Targets.Add(comp);
            }

            OutOfRange = Targets.FindAll(x => Vector3.Distance(transform.position, x.transform.position) > InteractRadius);

            foreach (InteractableSceneObject x in OutOfRange)
            {
                x.InteractionDeselect(HostInventory.InventoryOwner);
                Targets.Remove(x);
            }

            if (Targets.Count < 1) return;
            if (Targets.Count > 1) Targets = Targets.OrderBy(x => x.InteractionPriority).ToList();
            Targets[0].InteractionSelect(HostInventory.InventoryOwner);
        }

        /// <summary>
        /// Interact with the topmost priority object in the area.
        /// </summary>
        protected virtual void InteractWithCurrentTarget()
        {
            if (Targets.Count > 0) Targets[0].Interact(HostInventory.InventoryOwner);
        }
    }
}