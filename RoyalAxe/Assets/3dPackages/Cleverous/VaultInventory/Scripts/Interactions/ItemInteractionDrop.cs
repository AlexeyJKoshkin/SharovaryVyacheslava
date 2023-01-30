// (c) Copyright Cleverous 2023. All rights reserved.

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A Default Interaction. It will remove the item from the inventory and spawn it into the world.
    /// </summary>
    public class ItemInteractionDrop : Interaction
    {
        protected override void Reset()
        {
            base.Reset();
            Title = "Interact Drop";
            Description = "Drop the item immediately.";
            InteractLabel = "Drop";
        }

        public override bool IsValid(IInteractableUi target)
        {
            ItemUiPlug plug = target.MyTransform.GetComponent<ItemUiPlug>();
            if (plug == null) return false;
            if (plug.GetReferenceVaultItemData() == null) return false;

            // TODO check here to prevent this from being valid if it's not the local player's inventory.
            return true;
        }

        public override void DoInteract(IInteractableUi target)
        {
            ItemUiPlug plug = target.MyTransform.GetComponent<ItemUiPlug>();
            if (plug == null) return;

            plug.Ui.TargetInventory.CmdRequestDrop(plug.ReferenceInventoryIndex);
        }
    }
}