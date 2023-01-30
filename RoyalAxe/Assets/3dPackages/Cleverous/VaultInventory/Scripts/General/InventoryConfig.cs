// (c) Copyright Cleverous 2023. All rights reserved.

using Cleverous.VaultSystem;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Defines the configuration [or slot accessibility] of an <see cref="Inventory"/> through an arrangement of <see cref="SlotRestriction"/>s.
    /// </summary>
    public class InventoryConfig : DataEntity
    {
        [AssetDropdown(typeof(SlotRestriction))]
        public SlotRestriction[] SlotRestrictions;

        public InventoryConfig()
        {
            SlotRestrictions = new SlotRestriction[10];
        }

        protected override void Reset()
        {
            base.Reset();
            SlotRestrictions = new SlotRestriction[10];
        }
    }
}