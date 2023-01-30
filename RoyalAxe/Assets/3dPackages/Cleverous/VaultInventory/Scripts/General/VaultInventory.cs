// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Cleverous.NetworkImposter;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

#if MIRROR
using Mirror;
#elif FISHNET
#endif

namespace Cleverous.VaultInventory
{
    public static partial class VaultInventory
    {
        public enum ContextMode { MousePosition, PlugCorner, Custom }
        public enum TooltipMode { OnHoverOrSelect, OnEngage, Custom }

        public static Canvas GameCanvas;

        /// <summary>
        /// The generic prefab for all inventory slots.
        /// </summary>
        public static GameObject ItemSlotTemplate;

        /// <summary>
        /// The generic prefab for the 'floater' that is shown when mouse-dragging items between slots.
        /// </summary>
        public static GameObject ItemFloaterTemplate;

        /// <summary>
        /// The generic prefab for spawning items into the world space.
        /// </summary>
        public static GameObject RuntimeItemTemplate;

        /// <summary>
        /// The generic prefab for representing an Inventory in a UI panel.
        /// </summary>
        public static GameObject GenericInventoryUi;

        /// <summary>
        /// When false, <see cref="ItemUiPlug"/>s cannot be dragged. Clicking A->B is the only valid method.
        /// </summary>
        public static bool CanDragPlugs = true;

        /// <summary>
        /// If any UI is considered a "blocking panel" (for instance, blocking the player from moving while it is open) and is open then this will return true.
        /// </summary>
        public static bool AnyBlockingUiMenuIsOpen => BlockingUiPanels.Any(x => x.GetIsBlocking());

        /// <summary>
        /// A list of all UI panels that have registered as an active <see cref="UiBlockingPanel"/>.
        /// </summary>
        public static List<UiBlockingPanel> BlockingUiPanels;

        /// <summary>
        /// <para>MousePosition Mode: The Context Menu will appear with the top left corner at the mouse position.</para>
        /// <para>PlugCorner Mode: the Context Menu will appear with the top left corner at the top left corner position of the Plug that it is representing.</para>
        /// <para>Custom Mode: The system does nothing, you implement how this is handled.</para>
        /// </summary>
        public static ContextMode ContextStyle = ContextMode.PlugCorner;

        /// <summary>
        /// <para>OnHoverOrSelect Mode: The Tooltip will show for the item when the plug is hovered or selected.</para>
        /// <para>OnEngage Mode: The Tooltip will show for the item only when the plug is Engaged.</para>
        /// <para>Custom Mode: The system does nothing, you implement how this is handled.</para>
        /// </summary>
        public static TooltipMode TooltipStyle = TooltipMode.OnEngage;


        // ########### Static Session Events
        /// <summary>
        /// Should be fired when the player is spawned.
        /// </summary>
        public static Action<IUseInventory> OnPlayerSpawn;
        /// <summary>
        /// Should be fired when the scene changes.
        /// </summary>
        public static Action OnStartSceneChange;
        /// <summary>
        /// Fired by Vault when a UI item begins being lifted.
        /// </summary>
        public static Action<ItemUiPlug> OnMoveItemBegin;
        /// <summary>
        /// Fired by Vault when a UI move was canceled.
        /// </summary>
        public static Action<ItemUiPlug> OnMoveItemCancel;
        /// <summary>
        /// Fired by Vault when a lifted UI item is dropped.
        /// </summary>
        public static Action<ItemUiPlug> OnMoveItemEnd;
        /// <summary>
        /// Fired by Vault when a UI plug slot is selected by input.
        /// </summary>
        public static Action<ItemUiPlug> OnSlotSelected;
        // ###########


        /// <summary>
        /// Set runtime references. Required for proper operation.
        /// </summary>
        /// <param name="canvas">The Game Canvas</param>
        /// <param name="itemSlot">A Prefab for the ItemSlot's in the Inventory UI</param>
        /// <param name="itemFloater">A Prefab for the ItemFloater (items being dragged around) in the Inventory UI</param>
        /// <param name="itemRuntime">A Prefab for the Runtime items spawned into the world. Should have a RuntimeItemProxy script on it.</param>
        /// <param name="genericInventory">A Prefab for a generic inventory - used when opening to view contents of other inventories like crates, etc.</param>
        /// <param name="contextStyle">How you want the Context Menu to appear.</param>
        /// <param name="tooltipStyle">How you want tooltips to appear.</param>
        /// <param name="canDragPlugs">Whether dragging ui plugs is allowed or not. (only applies to mouse interactions)</param>
        public static void Initialize(
            Canvas canvas, GameObject itemSlot, GameObject itemFloater, GameObject itemRuntime, 
            GameObject genericInventory, ContextMode contextStyle, TooltipMode tooltipStyle, bool canDragPlugs)
        {
            GameCanvas = canvas;
            ItemSlotTemplate = itemSlot;
            ItemFloaterTemplate = itemFloater;
            RuntimeItemTemplate = itemRuntime;
            GenericInventoryUi = genericInventory;
            ContextStyle = contextStyle;
            TooltipStyle = tooltipStyle;
            CanDragPlugs = canDragPlugs;
        }

        /// <summary>
        /// Spawns a new item into the world. Creates a wrapper object from the template, spawns the art as a child and assigns the correct properties.
        /// </summary>
        /// <param name="item">The item to spawn.</param>
        /// <param name="pos">Target position to spawn at.</param>
        /// <param name="stackSize">Stack Size of the spawned item.</param>
        /// <returns>Returns the RuntimeItemProxy component on the wrapper if successful.</returns>
        public static RuntimeItemProxy SpawnWorldItem(RootItem item, Vector3 pos, int stackSize)
        {
            if (!NetworkPipeline.StaticIsServer())
            {
                Debug.LogError("Network Server error when trying to spawn runtime items.");
                return null;
            }

            if (item == null || item.ArtPrefab == null)
            {
                Debug.LogError("Failed SpawnWorldItem(). The input Source item or Art Prefab object was null when trying to SpawnWorldItem().");
                return null;
            }

            GameObject wrapper = Object.Instantiate(RuntimeItemTemplate, pos, Quaternion.identity);
            RuntimeItemProxy itemComponent = wrapper.GetComponent<RuntimeItemProxy>();

            if (itemComponent == null)
            {
                Debug.LogError("Failed SpawnWorldItem(). No RuntimeItemProxy component found. GameObject is floating garbage. You must add a RuntimeItemProxy to the Item Template Prefab.", wrapper);
                return null;
            }

            itemComponent.SvrInitialize(item, stackSize); // sets the syncvar values.
            NetworkPipeline.Spawn(wrapper);
            return itemComponent;
        }

        /// <summary>
        /// A generic way to spawn a UI into the game to show the content of an <see cref="Inventory"/>.
        /// </summary>
        /// <param name="bindTo">The Inventory that you want this panel to represent.</param>
        /// <returns>The spawned GameObject</returns>
        public static GameObject SpawnInventoryUi(Inventory bindTo)
        {
            // Spawn the UI, check the top level object, then dig deeper if there is nothing found.
            GameObject go = Object.Instantiate(GenericInventoryUi, GameCanvas.transform);
            InventoryUi ui = go.GetComponent<InventoryUi>();
            if (ui == null) ui = go.GetComponentInChildren<InventoryUi>();

            // Bind the UI to the given inventory.
            ui.SetTargetInventory(bindTo);
            return go;
        }

        /// <summary>
        /// Give an item to a target inventory.
        /// </summary>
        /// <param name="target">Target Inventory to give the item to</param>
        /// <param name="stack">The item to give</param>
        /// <returns>Any remainder of stack. Zero is complete success. -1 is an error.</returns>
        public static int TryGiveItem(Inventory target, RootItemStack stack)
        {
            if (stack != null && target != null) return target.DoAdd(stack);
            Debug.LogError("Error in TryGiveItem() call.");
            return -1;
        }

        public static void RegisterBlockingPanel(UiBlockingPanel p)
        {
            if (BlockingUiPanels == null)
            {
                BlockingUiPanels = new List<UiBlockingPanel>();
            }
            BlockingUiPanels.Add(p);
        }
        public static void DeregisterBlockingPanel(UiBlockingPanel p)
        {
            BlockingUiPanels.Remove(p);
        }

        
        // ########### Inputs are hardcoded and obtained through here for now. Later we'll probably add a custom input class for the EventSystem to read generically.
        /// <summary>
        /// Find out if the 'Select' key was pressed this frame. Used for confirming things, pressing buttons, etc.
        /// </summary>
        public static bool GetPressetSelect()
        {
            return Mouse.current.leftButton.wasReleasedThisFrame || (Gamepad.current != null && Gamepad.current.buttonSouth.wasReleasedThisFrame);
        }

        /// <summary>
        /// Find out if the 'Context' key was pressed this frame. Used for making the Context Menu appear/disappear.
        /// </summary>
        public static bool GetPressedContext()
        {
            return Mouse.current.rightButton.wasReleasedThisFrame || (Gamepad.current != null && Gamepad.current.buttonWest.wasReleasedThisFrame);
        }

        /// <summary>
        /// Find out if the 'Toggle Inventory' key was pressed this frame. Used for Opening and Closing inventory UI.
        /// </summary>
        public static bool GetPressedToggleInventory()
        {
            return Keyboard.current.tabKey.wasReleasedThisFrame || (Gamepad.current != null && Gamepad.current.buttonNorth.wasReleasedThisFrame);
        }        
        
        /// <summary>
        /// Find out if the 'Interact' key was pressed this frame. Used for directly interacting with world objects.
        /// </summary>
        public static bool GetPressedInteract()
        {
            return Keyboard.current.spaceKey.wasReleasedThisFrame || (Gamepad.current != null && Gamepad.current.buttonEast.wasReleasedThisFrame);
        }

        /// <summary>
        /// Find the mouse position. Can be used as a layer to return a fake mouse position if you prefer.
        /// </summary>
        public static Vector2 GetMousePosition()
        {
            return Mouse.current.position.ReadValue();
        }

        /// <summary>
        /// Change how the Context Menu appears.
        /// </summary>
        public static void SetContextStyle(ContextMode style)
        {
            ContextStyle = style;
        }

        /// <summary>
        /// Change how the Tooltip appears.
        /// </summary>
        public static void SetTooltipStyle(TooltipMode style)
        {
            TooltipStyle = style;
        }
    }
}