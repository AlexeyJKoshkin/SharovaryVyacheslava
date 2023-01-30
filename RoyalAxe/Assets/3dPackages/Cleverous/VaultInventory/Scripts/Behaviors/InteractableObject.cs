// (c) Copyright Cleverous 2023. All rights reserved.

using Cleverous.NetworkImposter;

#if MIRROR
using Mirror;
#elif FISHNET
using FishNet.Object;
using Command = FishNet.Object.ServerRpcAttribute;
#endif

namespace Cleverous.VaultInventory
{
    public abstract class InteractableSceneObject : NetworkBehaviour
    {
        public static InteractableSceneObject CurrentSelected;
        public bool IsSelected { get; protected set; }
        public int InteractionPriority { get; protected set; }
        public Interaction[] Interactions { get; protected set; }
        public abstract void Interact(IUseInventory interactor = null);
        public virtual void InteractionSelect(IUseInventory interactor)
        {
            if (CurrentSelected != null) CurrentSelected.InteractionDeselect(interactor);
            IsSelected = true;
        }
        public virtual void InteractionDeselect(IUseInventory interactor = null)
        {
            IsSelected = false;
        }
    }
}