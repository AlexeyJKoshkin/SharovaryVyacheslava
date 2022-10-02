using System.Collections.Generic;
using UnityEngine;

namespace RoyalAxe.Units
{
    public abstract class BaseUnitView : MonoBehaviour
    {
        [field: SerializeField] public Transform RootTransform { get; private set; }
        public abstract IEnumerable<IViewEntityBehaviour> EntityBehaviours();
    }
}