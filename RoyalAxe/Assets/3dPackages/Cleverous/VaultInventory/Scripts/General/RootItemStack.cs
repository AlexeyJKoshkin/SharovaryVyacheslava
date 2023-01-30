// (c) Copyright Cleverous 2023. All rights reserved.

using Cleverous.VaultSystem;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A stack of <see cref="RootItem"/>s. Contains a reference to the source data and an int to represent the stack size.
    /// </summary>
    public class RootItemStack
    {
        public RootItemStack(RootItem item, int count)
        {
            Source = item;
            StackSize = count;
        }
        public RootItemStack(int vaultId, int count)
        {
            Source = (RootItem) Vault.Get(vaultId);
            StackSize = count;
        }

        public RootItem Source;
        public int StackSize;

        public virtual void Reset()
        {
            Source = null;
            StackSize = 0;
        }

        /// <summary>
        /// Get the total market value of this stack
        /// </summary>
        /// <returns>Market Value of the stack.</returns>
        public virtual int GetTotalValue()
        {
            return Source.Value * StackSize;
        }
    }
}