using System;
using UnityEngine;

namespace RoyalAxe.Map
{
    /// <summary>
    ///     Настройки тайла
    /// </summary>
    [Serializable]
    public struct TileSettings
    {
        public float CellSizeInPixel;
     
        public Vector2 StartPoint;

        /// <summary>
        ///     Gets the width half of a tile.
        /// </summary>
        /// <value>width/2.</value>
        public float CellHalf => CellSizeInPixel * 0.5f;

    }
}