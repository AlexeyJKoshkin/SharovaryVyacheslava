using System.Collections.Generic;
using RoyalAxe.Units.UnitBehaviour;
using UnityEngine;

namespace RoyalAxe.Units
{
    public class UnitsView : BaseUnitView
    {
        [field: SerializeField] public Canvas UnitCanvas { get; private set; }
        [field: SerializeField] public MeleeTriggerHandler MeleeTriggerHandler { get; private set; }

        [field: SerializeField] public AbstractUnitBehaviour Behaviour { get; private set; }

        [SerializeField] private HealthBarTempComponent _healthBarTemp = new HealthBarTempComponent();

        [SerializeReference] private IAnimationUnitViewBuilder _spineEnemyAnimation;

        public override IEnumerable<IViewEntityBehaviour> EntityBehaviours()
        {
            yield return MeleeTriggerHandler;
            yield return _healthBarTemp;
            yield return _spineEnemyAnimation;
            yield return Behaviour;
        }
    }
}