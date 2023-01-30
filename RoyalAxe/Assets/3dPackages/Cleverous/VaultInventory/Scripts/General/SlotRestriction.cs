// (c) Copyright Cleverous 2023. All rights reserved.

using Cleverous.VaultSystem;
using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Used for reference comparisons to permit <see cref="RootItemStack"/>s to be placed in particular <see cref="ItemUiPlug"/> slots in an <see cref="Inventory"/>.
    /// </summary>
    public class SlotRestriction : DataEntity
    {
        [SpritePreview]
        public Sprite UiIcon;
    }
}