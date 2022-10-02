using System.Collections.Generic;
using RoyalAxe.Units.UnitBehaviour;
using UnityEngine;

namespace RoyalAxe.Units
{
    public class BosonView : BaseUnitView
    {
        public SpriteRenderer SpriteRenderer;
        [field: SerializeField] public MeleeTriggerHandler MeleeTriggerHandler { get; private set; }
        [field: SerializeField] public AbstractUnitBehaviour Behaviour { get; private set; }

        public override IEnumerable<IViewEntityBehaviour> EntityBehaviours()
        {
            yield return MeleeTriggerHandler;
            yield return Behaviour;
        }
    }
}