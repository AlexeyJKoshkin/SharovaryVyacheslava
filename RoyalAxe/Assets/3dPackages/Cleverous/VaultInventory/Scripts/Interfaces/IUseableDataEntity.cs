// (c) Copyright Cleverous 2023. All rights reserved.

using Cleverous.VaultSystem;
using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// An interface to interact with a <see cref="DataEntity"/> that can be "used".
    /// </summary>
    public interface IUseableDataEntity
    {
        int GetDbKey();
        string Title { get; set; }
        string Description { get; set; }
        Sprite UiIcon { get; set; }
        float UseCooldownTime { get; set; }
        void UseBegin(IUseInventory user);
        void UseFinish(IUseInventory user);
        void UseCancel(IUseInventory user);
    }
}