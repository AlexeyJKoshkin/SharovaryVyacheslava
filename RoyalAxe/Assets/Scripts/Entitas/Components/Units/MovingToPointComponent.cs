using System;
using Entitas;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    [Skill]
    [Units]
    public class MovingToPointComponent : IComponent
    {
        public IPointAdapter PointAdapter;

        public bool IsRichPosition(Vector2 currentPosition)
        {
            return PointAdapter.IsRichPosition(currentPosition);}

        public Vector2 TargetPosition => PointAdapter.TargetPosition;
    }

    public interface IPointAdapter
    {
        Vector2 TargetPosition { get; }
        bool IsRichPosition(Vector2 currentPosition);
    }

    public abstract class AbstractPointAdapter : IPointAdapter
    {
        private const float MAX_SQUARE_MAGNITUTE = 0.01f;
        public abstract Vector2 TargetPosition { get; }

        public bool IsRichPosition(Vector2 currentPosition)
        {
            return (TargetPosition - currentPosition).sqrMagnitude < MAX_SQUARE_MAGNITUTE;
        }
    }

    [Serializable]
    public class SimpleVector2Adapter : AbstractPointAdapter
    {
        public override Vector2 TargetPosition { get; }

        public SimpleVector2Adapter(Vector2 vector2)
        {
            TargetPosition = vector2;
        }
    }

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
    
    [Serializable]
    public class FollowUnitPointAdapter : AbstractPointAdapter
    {
        private readonly UnitsEntity _unitEntity;
        public override Vector2 TargetPosition => Target.position;
        public UnitsEntity Unit => _unitEntity;

        public Transform Target;

        public FollowUnitPointAdapter(UnitsEntity unitEntity)
        {
            _unitEntity = unitEntity;
            if (_unitEntity.hasUnitsView)
                Target = unitEntity.unitsView.RootTransform;
        }
    }

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