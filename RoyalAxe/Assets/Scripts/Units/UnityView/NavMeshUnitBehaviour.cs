using System;
using Entitas;
using UnityEngine;
using UnityEngine.AI;

namespace RoyalAxe.Units
{
    [Serializable]
    public class NavMeshUnitBehaviour : IViewEntityBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public void InitEntity(IEntity entity)
        {
            if(_navMeshAgent == null) return;
            if (entity is UnitsEntity unitsEntity)
            {
                _navMeshAgent.updateRotation = false;
                _navMeshAgent.updateUpAxis = false;
                _navMeshAgent.updatePosition = true;
                unitsEntity.AddNavMeshAgent(_navMeshAgent,unitsEntity.moveSpeed.CurrentValue);
            }
        }
    }
    
    
    
    
}
