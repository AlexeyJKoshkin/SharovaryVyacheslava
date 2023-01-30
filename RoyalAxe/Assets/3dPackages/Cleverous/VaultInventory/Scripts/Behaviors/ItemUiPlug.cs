// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A robust class to represent a "slot" in an <see cref="Inventory"/>. Takes full control of many built in interfaces and manages gamepad/keyboard/mouse interactions.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemUiPlug : Selectable, IInteractableUi, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler, ISubmitHandler
    {
        public Transform MyTransform => transform;
        public Interaction[] Interactions
        {
            get
            {
                if (GetReferenceVaultItemData() == null || GetReferenceVaultItemData().ExtraInteractions == null) return Array.Empty<Interaction>();
                return GetReferenceVaultItemData().ExtraInteractions;
            }
        }

        [Header("Plug References")]
        public GameObject SlotOwnerObject;
        public Image MyTypeImage;
        public Image MyItemImage;
        public Image MyHighlight;
        public Image MyEngagedBorder;
        public GameObject StackSizeBox;
        public TMP_Text StackSizeText;

        private Color m_oriBgColor;

        /// <summary>
        /// The UI hosting this Plug.
        /// </summary>
        public InventoryUi Ui { get; set; }
        /// <summary>
        /// A reference to the index of the item in the <see cref="Inventory"/> of the <see cref="InventoryUi"/> hosting this Plug - *not* a reference to the Vault Database index.
        /// </summary>
        public int ReferenceInventoryIndex { get; set; }

        /// <summary>
        /// A reference to the <see cref="RootItem"/> in the database.
        /// </summary>
        public virtual RootItem GetReferenceVaultItemData()
        {
            return Ui?.TargetInventory?.Get(ReferenceInventoryIndex)?.Source;
        }

        protected override void Awake()
        {
            base.Awake();
            m_oriBgColor = MyTypeImage == null ? Color.white : MyTypeImage.color;
            HighlightOff();
        }




        /// <summary>
        /// A Unity call. This is called when a Drag starts. It calls only on the component it is hovering.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (!VaultInventory.CanDragPlugs
                || eventData.button != PointerEventData.InputButton.Left
                || Ui.TargetInventory.Get(ReferenceInventoryIndex) == null 
                || Ui.TargetInventory.Get(ReferenceInventoryIndex).StackSize == 0) return;

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
        /// A Unity interface call. This is called on the same component OnBeginDrag() was called on. It is called every frame until the drag finishes.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!VaultInventory.CanDragPlugs 
                || eventData.button != PointerEventData.InputButton.Left
                || InventoryUi.DragFloater == null) return;

            InventoryUi.DragFloater.transform.position = eventData.position;
        }

        /// <summary>
        /// A Unity interface call. This is NOT called on the same component as the OnDrag() entity. It will be called on whatever component the drag ENDS hovering over.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnDrop(PointerEventData eventData)
        {
            if (!VaultInventory.CanDragPlugs
                || eventData.button != PointerEventData.InputButton.Left
                || InventoryUi.DragFloater == null) return;

            InventoryUi.DragDestination = this;
            VaultInventory.OnMoveItemEnd?.Invoke(this);
        }

        /// <summary>
        /// A Unity interface call. This is called on the same component OnBeginDrag() was called on. OnDrop() will always be called before this, and always at the end of a Drag.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (!VaultInventory.CanDragPlugs
                || eventData.button != PointerEventData.InputButton.Left
                || InventoryUi.DragFloater == null) return;

            InventoryUi.HandleDragEvent();
        }





        /// <summary>
        /// Selectable override. Used to highlight cells when the mouse hovers over them.
        /// </summary>
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (InventoryUi.DragFloater == null && VaultInventory.TooltipStyle == VaultInventory.TooltipMode.OnHoverOrSelect)
            {
                UiTooltip.Instance.Show(this);
            }
            HighlightOn();
        }

        /// <summary>
        /// Selectable override. Used to unhighlight cells when the mouse hovers away from them.
        /// </summary>
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (VaultInventory.TooltipStyle == VaultInventory.TooltipMode.OnHoverOrSelect)
            {
                UiTooltip.Instance.Hide();
            }
            HighlightOff();
        }

        /// <summary>
        /// <para>Selectable override. "Selecting" something in Unity is a passive 'hover' effect. For 'action' event on the selected object, OnSubmit() is used.</para>
        /// <para>This callback is used when any Selectable is [basically] hovered or chosen as the active target for potentially being pressed/activated.</para>
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            switch (VaultInventory.TooltipStyle)
            {
                case VaultInventory.TooltipMode.OnHoverOrSelect:
                    UiTooltip.Instance.Show(this);
                    break;
                case VaultInventory.TooltipMode.OnEngage:
                    UiTooltip.Instance.Hide();
                    break;
                case VaultInventory.TooltipMode.Custom:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            HighlightOn();
            VaultInventory.OnSlotSelected?.Invoke(this);
        }

        /// <summary>
        /// Selectable override. Called by Unity when something is no longer selected.
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnDeselect(BaseEventData eventData)
        {   
            base.OnDeselect(eventData);
            if (VaultInventory.TooltipStyle == VaultInventory.TooltipMode.OnHoverOrSelect)
            {
                UiTooltip.Instance.Hide();
            }
            HighlightOff();
        }



        /// <summary>
        /// A Unity interface call. Called when the entity is clicked. Not used for Gamepads/Keyboard interactions.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (InventoryUi.ClickedItem == null && GetReferenceVaultItemData() == null) return;

            // if you right click, we just interact.
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Interact();
                return;
            }

            Engage(eventData);
        }

        /// <summary>
        /// A Unity interface call. Called [basically] when UI entities are "clicked" by input that doesn't click (eg, something that is not a mouse).
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnSubmit(BaseEventData eventData)
        {
            Engage(eventData);
        }

        /// <summary>
        /// Entry point for generally trying to engage a slot plug.
        /// </summary>
        protected virtual void Engage(BaseEventData _)
        {
            // If you clicked the same object.
            if (InventoryUi.ClickedItem == this)
            {
                SetAsNotEngaged();
                VaultInventory.OnMoveItemCancel?.Invoke(this);
            }

            // if you clicked this first.
            else if (InventoryUi.ClickedItem == null)
            {
                SetAsEngaged();
                VaultInventory.OnMoveItemBegin?.Invoke(this);
            }

            // If you have an origin item and this isn't it. (this must be the destination)
            else if (InventoryUi.ClickedItem != null && InventoryUi.ClickedItem != this)
            {
                InventoryUi.DragOrigin = InventoryUi.ClickedItem;
                InventoryUi.DragDestination = this;
                InventoryUi.HandleDragEvent();
                VaultInventory.OnMoveItemEnd?.Invoke(this);
            }
        }



        public virtual void SetAsEngaged()
        {
            if (InventoryUi.ClickedItem != null) InventoryUi.ClickedItem.SetAsNotEngaged();
            InventoryUi.ClickedItem = this;
            MyEngagedBorder.gameObject.SetActive(true);

            if (VaultInventory.TooltipStyle == VaultInventory.TooltipMode.OnEngage)
            {
                UiTooltip.Instance.Show(this);
            }
        }
        public virtual void SetAsNotEngaged()
        {
            if (InventoryUi.ClickedItem == this) InventoryUi.ClickedItem = null;
            MyEngagedBorder.gameObject.SetActive(false);

            if (VaultInventory.TooltipStyle == VaultInventory.TooltipMode.OnEngage)
            {
                UiTooltip.Instance.Hide();
            }
        }



        /// <summary>
        /// Interact with this plug (by default will draw the context menu)
        /// </summary>
        public virtual void Interact(IUseInventory interactor = null)
        {
            UiContextMenu.Instance.ShowContextMenu(this);
        }
        public virtual void InteractionSelect()
        {
            // This is handled elsewhere through the UI classes and their interfaces. It can't be "selected" through this interface by default.
        }
        public virtual void InteractionDeselect()
        {            
            // This is handled elsewhere through the UI classes and their interfaces. It can't be "selected" through this interface by default.
        }


        /// <summary>
        /// Updates the visuals on the Plug to represent the given RootItemStack.
        /// </summary>
        /// <param name="content">The stack of data you want this Plug to represent.</param>
        /// <param name="restriction">The restriction of the slot.</param>
        public virtual void UpdateUi(RootItemStack content, SlotRestriction restriction)
        {
            string stack = string.Empty;
            Sprite itemImg = null;

            if (content != null)
            {
                if (content.Source != null) itemImg = content.Source.UiIcon;
                if (StackSizeBox != null) StackSizeBox.SetActive(content.StackSize > 1);
                stack = content.StackSize > 1 ? content.StackSize.ToString() : string.Empty;
            }

            // push the results to the ui
            if (MyItemImage != null)
            {
                SetItemSprite(itemImg);
                MyItemImage.color = itemImg == null ? Color.clear : Color.white;
            }

            if (MyTypeImage != null)
            {
                SetTypeSprite(restriction == null ? null : restriction.UiIcon);
                MyTypeImage.color = restriction == null || itemImg != null ? Color.clear : m_oriBgColor;
            }

            SetStackSizeText(stack);
        }

        public virtual void SetItemSprite(Sprite sprite)
        {
            if (MyItemImage == null) return;

            MyItemImage.sprite = sprite;
            MyItemImage.color = sprite == null ? Color.clear : Color.white;
        }        
        public virtual Sprite GetItemSprite()
        {
            return MyItemImage == null ? null : MyItemImage.sprite;
        }

        public virtual void SetTypeSprite(Sprite sprite)
        {
            if (MyTypeImage != null) MyTypeImage.sprite = sprite;
        }
        public virtual Sprite GetTypeSprite()
        {
            return MyTypeImage == null ? null : MyTypeImage.sprite;
        }

        public virtual void SetStackSizeText(string text)
        {
            if (StackSizeText == null) return;

            if (text == string.Empty) StackSizeBox.SetActive(text != string.Empty);
            StackSizeText.text = text;
        }
        public virtual string GetStackSizeText()
        {
            return StackSizeText == null ? string.Empty : StackSizeText.text;
        }

        public virtual void HighlightOn()
        {
            MyHighlight.gameObject.SetActive(true);
        }
        public virtual void HighlightOff()
        {
            MyHighlight.gameObject.SetActive(false);
        }
    }
}