// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A special <see cref="ItemUiPlug"/> with overrides to facilitate "hotbar" type behavior on the slots.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class HotbarUiPlug : ItemUiPlug
    {
        [Header("Hotbar Plug References")]
        public HotbarUiPanel HotbarPanel;
        public Image CooldownFillImage;
        public Action<int> OnChanged;
        private int m_dockId;

        public IUseableDataEntity TargetData;
        public bool IsOnCooldown;
        
        private float m_cooldownNormalized;
        private float m_cooldownTimeRemaining;

        public override RootItem GetReferenceVaultItemData()
        {
            // This is always null in the Hotbar. Docked content could be any DataEntity as long as it implements IUseableDataEntity.
            // Hotbar is not restricted to RootItem alone. Abilities, etc, could be in it too so there are considerations with that.
            return null;
        }

        protected override void Awake()
        {
            base.Awake();
            UpdateUi(null, null);
            if (CooldownFillImage != null) CooldownFillImage.fillAmount = 0;
        }
        protected virtual void Update()
        {
            if (m_cooldownTimeRemaining < 0 || TargetData == null)
            {
                IsOnCooldown = false;
                if (CooldownFillImage != null) CooldownFillImage.fillAmount = 0;
                return;
            }

            IsOnCooldown = true;
            m_cooldownTimeRemaining -= Time.deltaTime;
            m_cooldownNormalized = m_cooldownTimeRemaining / TargetData.UseCooldownTime;
            if (CooldownFillImage != null) CooldownFillImage.fillAmount = m_cooldownNormalized;
        }

        /// <summary>
        /// A Unity call. This is called when a Drag starts. It calls only on the component it is hovering.
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (HotbarPanel.IsLocked) return;

            // can't drag the nothingness.
            if (TargetData == null) return;

            // can't drag if already dragging.
            if (InventoryUi.DragFloater != null) Destroy(InventoryUi.DragFloater);

            // can't have a tooltip if you're dragging.
            UiTooltip.Instance.Hide();

            InventoryUi.DragFloater = Instantiate(VaultInventory.ItemFloaterTemplate, transform);
            InventoryUi.DragFloater.transform.SetParent(VaultInventory.GameCanvas.transform);
            ItemUiFloater x = InventoryUi.DragFloater.GetComponent<ItemUiFloater>();
            x.Set(GetItemSprite(), GetStackSizeText());
            InventoryUi.DragOrigin = this;

            VaultInventory.OnMoveItemBegin?.Invoke(this);
        }

        /// <summary>
        /// A Unity interface call. Called when the entity is clicked.
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerClick(PointerEventData eventData)
        {
            // clicking an empty cell first should do nothing.
            if (InventoryUi.ClickedItem == null && TargetData == null) return;

            // left clicks
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (HotbarPanel.IsLocked)
                {
                    Interact();
                    return;
                }
                Engage(eventData);
                return;
            }

            // right clicks
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (HotbarPanel.IsLocked)
                {
                    // nothing happens.
                    return;
                }
                // clear plug
                UpdateUiForHotbarPlug(null);
            }
        }

        public override void Interact(IUseInventory interactor = null)
        {
            if (IsOnCooldown || TargetData == null) return;

            RootItem item = (RootItem)TargetData;
            if (item != null && !HotbarPanel.Owner.Inventory.Contains(item, 1)) return;

            TargetData.UseBegin(HotbarPanel.Owner);
            m_cooldownTimeRemaining = TargetData.UseCooldownTime;
        }

        public override void UpdateUi(RootItemStack content, SlotRestriction restriction)
        {
            if (HotbarPanel == null || HotbarPanel.IsLocked) return;

            if (content == null || content.Source == null)
            {
                UpdateUiForHotbarPlug(null);
                return;
            }

            if (content.Source.GetType().IsAssignableFrom(typeof(IUseableDataEntity)))
            {
                UpdateUiForHotbarPlug((IUseableDataEntity)content.Source);
            }
        }
        public virtual void UpdateUiForHotbarPlug(IUseableDataEntity data)
        {
            if (HotbarPanel.IsLocked) return;

            TargetData = data;

            if (TargetData == null)
            {
                base.UpdateUi(null, null);
            }
            else
            {
                int stacksize = HotbarPanel.Owner.Inventory.GetCountOfItem(data.GetDbKey());
                SetTypeSprite(null);
                SetItemSprite(data.UiIcon);
                SetStackSizeText(stacksize > 0 ? stacksize.ToString() : "");
            }

            OnChanged?.Invoke(m_dockId);
        }
        public virtual void Clear()
        {
            SetTypeSprite(null);
            SetItemSprite(null);
            SetStackSizeText(string.Empty);
            StackSizeBox.SetActive(false);
            TargetData = null;
        }
        public virtual void SetDockId(int id)
        {
            m_dockId = id;
        }
    }
}