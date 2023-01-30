// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// For serializing and deserializing an <see cref="Inventory"/> and it's content from or to a particular state.
    /// </summary>
    [Serializable]
    public partial class InventoryState
    {  
        /// <summary>
        /// The Vault Index ID of the Inventory Configuration.
        /// </summary>
        [FormerlySerializedAs("ConfigIndex")]
        public int ConfigDbKey;
        /// <summary>
        /// A list of Vault Index IDs to identify items in each slot.
        /// </summary>
        [FormerlySerializedAs("ItemIndexes")]
        public List<int> ItemDbKeys;
        /// <summary>
        /// A list of int's to indicate the stack size in each slot.
        /// </summary>
        public List<int> ItemStackCounts;

        public InventoryState(Inventory source, List<RootItemStack> content)
        {
            ConfigDbKey = source.Configuration.GetDbKey();
            ItemDbKeys = new List<int>();
            ItemStackCounts = new List<int>();

            foreach (RootItemStack t in content)
            {
                ItemDbKeys.Add(t != null ? t.Source.GetDbKey() : -1);
                ItemStackCounts.Add(t != null ? t.StackSize : 0);
            }
        }

        /// <summary>
        /// A simple way to JSON an <see cref="InventoryState"/> from an <see cref="Inventory"/>.
        /// </summary>
        public virtual string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }
        /// <summary>
        /// A simple way to get an <see cref="InventoryState"/> from saved JSON which can be used in <see cref="Inventory"/>.Initialize().
        /// </summary>
        public static InventoryState FromJson(string json)
        {
            return JsonUtility.FromJson<InventoryState>(json);
        }
    }
}