// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Cleverous.VaultSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// The UI Context Menu is a popup for contextual actions on some entity. Buttons are automatically populated from the given Prefab.
    /// </summary>
    public class UiContextMenu : MonoBehaviour
    {
        public static UiContextMenu Instance;
        public static Action OnOpened;
        public static Action OnClosed;
        public bool MenuIsOpen { get; protected set; }

        [AssetDropdown(typeof(ContextMenuConfig))]
        public ContextMenuConfig Configuration;
        public GameObject ButtonPrefab;
        public GameObject PanelWrapper;

        protected List<UiContextMenuButton> UiButtons;
        protected List<Interaction> ValidInteractions;
        protected IInteractableUi CurrentTarget;

        private RectTransform m_rect;
        private List<Interaction> m_defaultInteractions;
        
        public virtual void Awake()
        {
            Instance = this;
            PanelWrapper.gameObject.SetActive(false);
            UiButtons = new List<UiContextMenuButton>();

            m_rect = GetComponent<RectTransform>();
            m_defaultInteractions = new List<Interaction>
            {
                Configuration.UseInteraction,
                Configuration.SplitInteraction,
                Configuration.DropInteraction
            };
        }
        protected virtual void Update()
        {
            if (!MenuIsOpen) return;

            if (VaultInventory.GetPressedContext()) HideContextMenu();

            // legacy mouse check
            /*
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue()
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            bool isHoveringContextMenu = false;
            foreach (RaycastResult x in results)
            {
                if (UiButtons.Any(o => o.gameObject == x.gameObject))
                {
                    isHoveringContextMenu = true;
                }
            }
            if (!isHoveringContextMenu) 
            {
                HideContextMenu();
            }
            */

        }

        /// <summary>
        /// Show the context menu on screen. If there is only one possible interaction, it is fired.
        /// </summary>
        /// <param name="target">The thing to interact with - what the dropdown content is based on.</param>
        public virtual void ShowContextMenu(IInteractableUi target)
        {
            if (target.MyTransform == null) return;
            CurrentTarget = target;

            // find out how many valid interactions there are
            ValidInteractions = new List<Interaction>();

            foreach (Interaction x in m_defaultInteractions)
            {
                if (!x.IsValid(CurrentTarget)) continue;
                ValidInteractions.Add(x);
            }
            foreach (Interaction x in target.Interactions)
            {
                if (!x.IsValid(CurrentTarget)) continue;
                ValidInteractions.Add(x);
            }

            // We can only do something if there's something to do.
            if (ValidInteractions.Count == 0) return;

            // If there is only one thing to do, then do it.
            if (ValidInteractions.Count == 1)
            {
                ValidInteractions[0].DoInteract(target);
                return;
            }



            // continue with the process....
            // get into correct position
            RectTransform rt = target.MyTransform.GetComponent<RectTransform>();
            Vector3 position = VaultInventory.ContextStyle == VaultInventory.ContextMode.MousePosition
                ? VaultInventory.GetMousePosition()
                : rt.position - new Vector3(rt.rect.width / 2, -(rt.rect.height / 2), 0);
            PanelWrapper.transform.position = position;



            // be the front-most visible panel
            m_rect.SetAsLastSibling();


            // If there are multiple things to do, clear and re-populate the list of buttons.
            foreach (UiContextMenuButton b in UiButtons)
            {
                Destroy(b.gameObject);
            }
            UiButtons = new List<UiContextMenuButton>();

            for (int i = 0; i < ValidInteractions.Count; i++)
            {
                GameObject go = Instantiate(ButtonPrefab, PanelWrapper.transform);
                UiContextMenuButton btn = go.GetComponent<UiContextMenuButton>();
                btn.SetText(ValidInteractions[i].InteractLabel);
                btn.SetIndex(i);
                UiButtons.Add(btn);
            }


            // set navigation
            for (int i = 0; i < UiButtons.Count; i++)
            {
                // first
                if (i == 0)
                {
                    UiButtons[i].SetNavigation(
                        UiButtons[ValidInteractions.Count-1].TargetButton,
                        UiButtons[i+1].TargetButton);
                }

                // last
                else if (i == ValidInteractions.Count-1)
                {
                    UiButtons[i].SetNavigation(
                        UiButtons[i-1].TargetButton,
                        UiButtons[0].TargetButton);
                }

                // middle
                else if (ValidInteractions.Count > 2)
                {
                    UiButtons[i].SetNavigation(
                        UiButtons[i - 1].TargetButton,
                        UiButtons[i + 1].TargetButton);
                }
            }

            if (InventoryUi.ClickedItem != null) InventoryUi.ClickedItem.SetAsNotEngaged();
            EventSystem.current.SetSelectedGameObject(UiButtons.First(x => x.gameObject.activeSelf).gameObject);
            
            MenuIsOpen = true;
            PanelWrapper.gameObject.SetActive(true);
            OnOpened?.Invoke();
        }

        /// <summary>
        /// Used implicitly by the <see cref="Button"/>s under the Grid on the Context Menu.
        /// </summary>
        /// <param name="id">The index of the button.</param>
        public virtual void ClickedInteractionUiButton(int id)
        {
            HideContextMenu();
            ValidInteractions[id].DoInteract(CurrentTarget);
        }

        /// <summary>
        /// Hide the context menu.
        /// </summary>
        public virtual void HideContextMenu()
        {
            if (CurrentTarget != null)
            {
                CurrentTarget.InteractionDeselect();
                EventSystem.current.SetSelectedGameObject(CurrentTarget.MyTransform.gameObject);
            }

            MenuIsOpen = false;
            PanelWrapper.gameObject.SetActive(false);
            OnClosed?.Invoke();
        }
    }
}