using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    
    /// <summary>
    /// Просто указатель что цель достигла цели
    /// </summary>
    public class RichPointAdapter : IPointAdapter
    {
        public Vector2 TargetPosition { get; set; }
        public bool IsRichPosition(Vector2 currentPosition)
        {
            TargetPosition = currentPosition;
            return true;
        }
    }
}
