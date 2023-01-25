using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public interface IPointAdapter
    {
        Vector2 TargetPosition { get; }
        bool IsRichPosition(Vector2 currentPosition);
    }
}
