// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine.EventSystems;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Used to populate a single item that a <see cref="Merchant"/> has for sale.
    /// </summary>
    public class MerchantUiSellSlot : ItemUiPlug
    {
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);

            if (!VaultInventory.CanDragPlugs
                || eventData.button != PointerEventData.InputButton.Left
                || InventoryUi.DragFloater == null) return;

            Sell();
        }

        protected override void Engage(BaseEventData eventData)
        {
            if (InventoryUi.ClickedItem != null && InventoryUi.ClickedItem != this)
            {
                InventoryUi.DragOrigin = InventoryUi.ClickedItem;
                Sell();
            }
        }

        private void Sell()
        {
            // If we're not dropping into the hotbar, try to sell the item.
            if (!(InventoryUi.DragOrigin is HotbarUiPlug))
            {
                MerchantUi.Instance.ClientSell(InventoryUi.DragOrigin.ReferenceInventoryIndex);
            }

            if (InventoryUi.DragFloater != null) Destroy(InventoryUi.DragFloater);
            if (InventoryUi.ClickedItem != null) InventoryUi.ClickedItem.SetAsNotEngaged();
            InventoryUi.DragOrigin = null;
            InventoryUi.DragDestination = null;
        }
    }
}