// (c) Copyright Cleverous 2023. All rights reserved.

using System;

#if MIRROR
using Mirror;
#elif FISHNET
using FishNet.Object;
using NetworkIdentity = FishNet.Object.NetworkObject;
#endif

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A Default <see cref="Interaction"/>. Will attempt to Use the target item via <see cref="UseableItem"/>.
    /// </summary>
    public class ItemInteractionUse : Interaction
    {
        protected override void Reset()
        {
            base.Reset();
            Title = "Interact Use";
            Description = "Use the item immediately.";
            InteractLabel = "USE";
        }

        public override bool IsValid(IInteractableUi target)
        {
            ItemUiPlug plug = target.MyTransform.GetComponent<ItemUiPlug>();
            if (plug == null) return false;
            if (plug.GetReferenceVaultItemData() == null) return false;

            return plug.GetReferenceVaultItemData() is UseableItem;
        }

        public override void DoInteract(IInteractableUi target)
        {
            ItemUiPlug plug = target.MyTransform.GetComponent<ItemUiPlug>();
            if (plug == null) return;

            // Only works for UseableItem classes.
            ((UseableItem)plug.GetReferenceVaultItemData()).UseBegin(plug.Ui.TargetInventory.InventoryOwner);
        }
    }
}