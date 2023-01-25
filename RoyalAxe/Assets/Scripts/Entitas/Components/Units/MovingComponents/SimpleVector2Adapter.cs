using System;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    //Движение к конкретной точке
    [Serializable]
    public class SimpleVector2Adapter : AbstractPointAdapter
    {
        public override Vector2 TargetPosition { get; }

        public SimpleVector2Adapter(Vector2 vector2)
        {
            TargetPosition = vector2;
        }
    }
}
