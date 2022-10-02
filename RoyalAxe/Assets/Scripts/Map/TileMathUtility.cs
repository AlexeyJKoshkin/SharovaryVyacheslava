using System;
using UnityEngine;

namespace RoyalAxe.Map
{
    /// <summary>
    ///     Вспомогательный класс, для мат расчетов в изо координатах
    /// </summary>
    public class TileMathUtility
    {
        public const int MAX_TILE = 10;

        /// <summary>
        ///     Начало юнити координат
        /// </summary>
        private static readonly Vector2 START_POINT = Vector2.zero;

        /// <summary>
        ///     Настройка тайла
        /// </summary>
        public static TileSettings TILE_SETTINGS { get; set; }


        /// <summary>
        ///     Переводит координату изометрической сетки в юнити вектор
        /// </summary>
        /// <returns></returns>
        public static Vector3 CellToUnity(CellCoordinate cell, TilePointType bottom)
        {
            return CellToUnity(cell.X, cell.Y, bottom);
        }

        public static Vector3 CellToUnity(int x, int y, TilePointType pointType)
        {
            Vector3 unityPosition = START_POINT;
            unityPosition.x = unityPosition.x + TILE_SETTINGS.CellSizeInPixel * x;
            unityPosition.y = unityPosition.y + TILE_SETTINGS.CellSizeInPixel * y;

            switch (pointType)
            {
                case TilePointType.Center:
                    unityPosition.y += TILE_SETTINGS.CellHalf;
                    unityPosition.x += TILE_SETTINGS.CellHalf;
                    break;

                case TilePointType.Left:
                    unityPosition.y += TILE_SETTINGS.CellHalf;
                    break;

                case TilePointType.Right:
                    unityPosition.x += TILE_SETTINGS.CellSizeInPixel;
                    unityPosition.y += TILE_SETTINGS.CellHalf;
                    break;

                case TilePointType.Top:
                    unityPosition.y += TILE_SETTINGS.CellSizeInPixel;
                    unityPosition.x += TILE_SETTINGS.CellHalf;
                    break;

                case TilePointType.Bottom: break;

                default: throw new ArgumentOutOfRangeException(nameof(pointType), pointType, null);
            }

            return unityPosition;
        }


        /// <summary>
        ///     Переводим из Юнити координат в исошные координаты
        /// </summary>
        /// <param name="unityCoordinate"></param>
        /// <returns></returns>
        public static CellCoordinate UnityToCell(Vector2 unityCoordinate)
        {
            var relativePos = unityCoordinate - START_POINT;
            var x           = relativePos.x / TILE_SETTINGS.CellSizeInPixel;
            var y           = unityCoordinate.y / TILE_SETTINGS.CellSizeInPixel;
            return new CellCoordinate(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
        }
    }
}