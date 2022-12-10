using Entitas;
using RoyalAxe.Units.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalAxe.Units {
    public class HealthBarUnitView : MonoBehaviour, IViewEntityBehaviour, IHealthListener
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
                e.AddHealthListener(this);
                OnHealth(e, e.health.UnitStatValue);
            }
        }


        public void OnHealth(UnitsEntity entity, CharacterStatValue health)
        {
            _healthBar.value = health.Value / health.MaxValue;
            _healthText.text = $"{health.Value}/{health.MaxValue}";
        }
    }
}