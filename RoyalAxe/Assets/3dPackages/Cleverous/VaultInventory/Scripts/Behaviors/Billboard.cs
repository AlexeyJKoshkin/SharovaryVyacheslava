// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Forces the object to look the same direction as the main camera. Uses Update. Not as efficient as a custom billboard shader.
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        private Camera m_cam;

        public void Awake()
        {
            m_cam = Camera.main;
        }

        public void Update()
        {
            transform.forward = m_cam.transform.forward;
        }
    }
}