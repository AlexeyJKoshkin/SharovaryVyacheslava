using Entitas;
using UnityEngine;
using UnityEngine.AI;

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

    [Units]
    public class NavMeshAgentComponent : IComponent
    {
        public NavMeshAgent NavMeshAgent;
        public float Speed
        {
            get => NavMeshAgent.speed;
            set => NavMeshAgent.speed = value;
        }

        public void SetDestinationPoint(Vector3 endPoint)
        {
            NavMeshAgent.SetDestination(endPoint);
        }
    }
}