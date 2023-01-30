// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// <para>Runtime component required in each game scene. Initializes UI templates and the Canvas.
    /// In your own project you might choose to create your own class that does the same thing 
    /// that fits into your game architecture better. </para>
    /// </summary>
    public class VaultRuntime : MonoBehaviour
    {
        [Header("Scene References")]
        [Tooltip("A reference to the main Canvas, used at runtime by various UI items.")]
        public Canvas GameCanvas;

        [Header("Project References")]
        [Tooltip("A template for Inventory UI empty slots, usually just an Image of a blank inventory slot.")]
        public GameObject ItemSlotTemplate;
        [Tooltip("A template for Inventory UI filled slots, usually just an Image which will contain the item Sprite and is filled at runtime by the back-end.")]
        public GameObject ItemFloaterTemplate;
        [Tooltip("A template for Items spawned into the world. This requires a working RuntimeItemProxy component and will be used to spawn all objects into the world.")]
        public GameObject RuntimeItemTemplate;
        [Tooltip("A generic inventory UI prefab that will can be used for things like representing the items in a chest you just opened.")]
        public GameObject GenericInventoryUi;

        protected virtual void Awake()
        {
            VaultInventory.Initialize(
                GameCanvas, ItemSlotTemplate, ItemFloaterTemplate, RuntimeItemTemplate, 
                GenericInventoryUi, VaultInventory.ContextMode.PlugCorner, VaultInventory.TooltipMode.OnEngage, true);
        }
    }
}