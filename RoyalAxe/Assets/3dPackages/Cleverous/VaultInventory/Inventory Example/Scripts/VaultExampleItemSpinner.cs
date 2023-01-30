// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine;

namespace Cleverous.VaultInventory.Example
{
    public class VaultExampleItemSpinner : MonoBehaviour
    {
        public void FixedUpdate()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 50);
        }
    }
}