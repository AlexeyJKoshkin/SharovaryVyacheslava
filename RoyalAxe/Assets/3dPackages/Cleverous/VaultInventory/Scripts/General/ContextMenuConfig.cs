// (c) Copyright Cleverous 2022. All rights reserved.

using Cleverous.VaultSystem;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A <see cref="DataEntity"/> which is intended to define the default interactions for all <see cref="IInteractableUi"/>s.
    /// </summary>
    public class ContextMenuConfig : DataEntity
    {
        [AssetDropdown(typeof(Interaction))]
        public Interaction UseInteraction;
        [AssetDropdown(typeof(Interaction))]
        public Interaction SplitInteraction;
        [AssetDropdown(typeof(Interaction))]
        public Interaction DropInteraction;
    }
}