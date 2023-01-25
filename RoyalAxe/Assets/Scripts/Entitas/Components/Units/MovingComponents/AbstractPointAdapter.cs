using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public abstract class AbstractPointAdapter : IPointAdapter
    {
        private const float MAX_SQUARE_MAGNITUTE = 0.01f;
        public abstract Vector2 TargetPosition { get; }

        public bool IsRichPosition(Vector2 currentPosition)
        {
            return (TargetPosition - currentPosition).sqrMagnitude < MAX_SQUARE_MAGNITUTE;
        }
    }
}
