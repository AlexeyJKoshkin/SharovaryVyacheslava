using System;
using Entitas;
using RoyalAxe.CharacterStat;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalAxe.Units
{
    //временная штуковина для хелсбара
    [Serializable]
    public class HealthBarTempComponent : IViewEntityBehaviour
    {
        private UnitsEntity _owner;
        [SerializeField] private Slider _healthBar;

        public void InitEntity(IEntity entity)
        {
            if (_healthBar == null)
            {
                return;
            }

            if (entity is UnitsEntity e)
            {
                _owner                =  e;
                e.OnComponentReplaced += EOnOnComponentReplaced;
            }
        }

        private void EOnOnComponentReplaced(IEntity entity, int index, IComponent previouscomponent, IComponent newcomponent)
        {
            if (index == UnitsComponentsLookup.Health)
            {
                ModifiableStat health = newcomponent as ModifiableStat;
                _healthBar.value = health.CurrentValue / health.MaxValue;
            }
        }
    }
}