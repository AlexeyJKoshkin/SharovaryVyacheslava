using System.Collections.Generic;
using RoyalAxe.Units.UnitBehaviour;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.Units
{
    public class UnitsView : BaseUnitView
    {
        [field: SerializeField]
        public Canvas UnitCanvas { get; private set; }
        [field: SerializeField]
        public MeleeTriggerHandler MeleeTriggerHandler { get; private set; }

        [field: SerializeField]
        public AbstractUnitBehaviour Behaviour { get; private set; }

        [HideLabel]
        [SerializeField] private NavMeshUnitBehaviour _navMeshUnit;
        [SerializeField] private HealthBarUnitView healthBarUnitTemp;

        [SerializeReference] private IAnimationUnitViewBuilder _spineEnemyAnimation;

        public override IEnumerable<IViewEntityBehaviour> EntityBehaviours()
        {
            yield return MeleeTriggerHandler;
            yield return healthBarUnitTemp;
            yield return _spineEnemyAnimation;
            yield return Behaviour;
            yield return _navMeshUnit;
        }
    }
}
