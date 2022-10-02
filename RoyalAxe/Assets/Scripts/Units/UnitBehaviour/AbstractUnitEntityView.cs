using Entitas;
using UnityEngine;

namespace RoyalAxe.Units.UnitBehaviour
{
    public abstract class AbstractUnitEntityView : MonoBehaviour, IViewEntityBehaviour
    {
        protected UnitsEntity Unit { get; private set; }

        public void InitEntity(IEntity entity)
        {
            if (entity is UnitsEntity u)
            {
                Unit = u;
            }

            OnInit();
        }

        protected abstract void OnInit();
    }
}