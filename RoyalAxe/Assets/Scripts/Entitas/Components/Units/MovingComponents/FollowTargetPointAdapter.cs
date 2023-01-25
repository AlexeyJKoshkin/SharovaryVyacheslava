using System;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    /// <summary>
    /// движение к трансформу. Фактически преследование цели 
    /// </summary>
    [Serializable]
    public class FollowTargetPointAdapter : AbstractPointAdapter
    {
        public override Vector2 TargetPosition => Target.position;

        public Transform Target;

        public FollowTargetPointAdapter(Transform transform)
        {
            Target = transform;
        }
    }
}
