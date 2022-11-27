using Entitas;
using RoyalAxe.CharacterStat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalAxe.Units {
    public class HealthBarUnitView : MonoBehaviour, IViewEntityBehaviour
    {
       
        [SerializeField] private Slider _healthBar;
        [SerializeField] private TextMeshProUGUI _healthText;
        
        public void InitEntity(IEntity entity)
        {
            if (_healthBar == null)
            {
                return;
            }

            if (entity is UnitsEntity e)
            {
      
                e.OnComponentReplaced += EOnOnComponentReplaced;
                e.OnDestroyEntity += EOnOnDestroyEntity;
            }
        }

        private void EOnOnDestroyEntity(IEntity entity)
        {
            entity.OnComponentReplaced -= EOnOnComponentReplaced;
        }

        private void EOnOnComponentReplaced(IEntity entity, int index, IComponent previouscomponent, IComponent newcomponent)
        {
            if (index == UnitsComponentsLookup.Health)
            {
                ModifiableStat health = newcomponent as ModifiableStat;
                _healthBar.value = health.CurrentValue / health.MaxValue;
                _healthText.text = $"{health.CurrentValue}/{health.MaxValue}";
            }
        }
    }
}