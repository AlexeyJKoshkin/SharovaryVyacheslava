// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// An easy way to reference an Inventory and interact with it.
    /// </summary>
    public interface IUseInventory
    {
        Inventory Inventory { get; set; }
        Transform MyTransform { get; }
    }
}