// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A manager control panel for a group of <see cref="HotbarUiPlug"/> slots.
    /// </summary>
    public class HotbarUiPanel : MonoBehaviour
    {
        public List<HotbarUiPlug> HotbarSlots;
        public GameObject PanelWrapper;

        /// <summary>
        /// Is the Hotbar locked? Locked Hotbar cannot be modified, but slotted entities can be interacted with.
        /// </summary>
        public bool IsLocked { get; protected set; }
        /// <summary>
        /// Is the Hotbar Menu open/visible?
        /// </summary>
        public bool IsOpen { get; protected set; }
        /// <summary>
        /// Called when the hotbar is shown/opened.
        /// </summary>
        public Action OnOpened;
        /// <summary>
        /// Called when the hotbar is hidden/closed.
        /// </summary>
        public Action OnClosed;
        /// <summary>
        /// Called when the hotbar is locked.
        /// </summary>
        public Action OnLocked;
        /// <summary>
        /// Called when the hotbar is unlocked.
        /// </summary>
        public Action OnUnlocked;

        /// <summary>
        /// Stores a list of Vault Data Indexes that indicate what entities are slotted in each hotbar slot.
        /// </summary>
        protected List<int> RuntimeIndexAssignments;

        /// <summary>
        /// The owner of this Hotbar.
        /// </summary>
        public IUseInventory Owner;

        protected virtual void Awake()
        {
            RuntimeIndexAssignments = new List<int>();
            VaultInventory.OnPlayerSpawn += SetOwner;
            for (int i = 0; i < HotbarSlots.Count; i++)
            {
                HotbarSlots[i].OnChanged += SlotWasModified;
                HotbarSlots[i].SetDockId(i);
            }

            Show();
        }
        protected virtual void Update()
        {
            if (!VaultInventory.AnyBlockingUiMenuIsOpen && HotbarSlots.Any(x => x.gameObject == EventSystem.current.currentSelectedGameObject))
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }


        /// <summary>
        /// Manually Initialize the Hotbar with an item set. Pulls data from the internal list of assignments. Extend class to add functionality to restore a state by setting that list.
        /// </summary>
        /// <param name="vaultDbKeys">A list of DB Keys. the List order (low to high) is the order on the hotbar (left to right).</param>
        public virtual void Initialize(List<int> vaultDbKeys)
        {
            for (int i = 0; i < vaultDbKeys.Count; i++)
            {
                AssignHotbarReference(i, vaultDbKeys[i]);
            }
        }
        /// <summary>
        /// Assign a specific slot to be a specific entity in the Vault. Can be used for serialization/preference restore purposes.
        /// </summary>
        /// <param name="hotbarIndex">The index of the Hotbar you want to assign.</param>
        /// <param name="vaultKey">The Vault Data Index of the DataEntity you want to slot in.</param>
        public virtual void AssignHotbarReference(int hotbarIndex, int vaultKey)
        {
            // Cannot assign an item type more than once. If you don't like this, comment out this line.
            if (RuntimeIndexAssignments.Contains(vaultKey)) return;

            RuntimeIndexAssignments[hotbarIndex] = vaultKey;
        }
        /// <summary>
        /// Set the Owner of the Hotbar. Handled during Initialize() but can be changed later.
        /// </summary>
        /// <param name="inv"></param>
        public virtual void SetOwner(IUseInventory inv)
        {
            Owner = inv;
        }
        /// <summary>
        /// Remotely use a slot. Useful for setting up character inputs to activate specific slot IDs.
        /// </summary>
        /// <param name="index"></param>
        public virtual void ActivateSlotRemotely(int index)
        {
            HotbarSlots[index].Interact();
        }

        /// <summary>
        /// Set the lock state of the Hotbar. Locked Hotbar cannot be modified, but slotted entities can be interacted with.
        /// </summary>
        /// <param name="isLocked"></param>
        public virtual void SetLockState(bool isLocked)
        {
            IsLocked = isLocked;
            if (IsLocked) OnLocked?.Invoke();
            else OnUnlocked?.Invoke();
        }
        /// <summary>
        /// Invert the current lock state of the Hotbar. Locked Hotbar cannot be modified, but slotted entities can be interacted with.
        /// </summary>
        public virtual void ToggleLockState()
        {
            SetLockState(!IsLocked);
        }

        /// <summary>
        /// Internal callback used when any slot changes. Checks against other slots to avoid duplicate object entries.
        /// </summary>
        /// <param name="dockId"></param>
        protected virtual void SlotWasModified(int dockId)
        {
            // TODO Niche exploit - put items on the hotbar again to circumvent cooldowns.
            for (int i = 0; i < HotbarSlots.Count; i++)
            {
                if (i == dockId) continue;
                if (HotbarSlots[i].TargetData == HotbarSlots[dockId].TargetData)
                {
                    HotbarSlots[i].Clear();
                }
            }
        }

        /// <summary>
        /// Show the Hotbar. This enables the PanelWrapper object.
        /// </summary>
        public virtual void Show()
        {
            PanelWrapper.SetActive(true);
            IsOpen = true;
            OnOpened?.Invoke();
        }
        /// <summary>
        /// Hide the Hotbar. This disables the PanelWrapper object.
        /// </summary>
        public virtual void Hide()
        {
            PanelWrapper.SetActive(false);
            IsOpen = false;
            OnClosed?.Invoke();
        }
    }
}