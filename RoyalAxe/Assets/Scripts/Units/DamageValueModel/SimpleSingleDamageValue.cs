using UnityEngine;

namespace RoyalAxe {
    class SimpleSingleDamageValue : IDamageValue
    {
        public float Damage
        {
            get => _defaultDamage;
            private set => _defaultDamage = Mathf.Max(0, value);
        }
        public float CriticalDamage
        {
            get => _criticalDamage;
            set => _criticalDamage = Mathf.Max(0, value);
        }

        public void IncreaseDamage(float delta)
        {
            Damage += delta;
        }

        private float _criticalDamage, _defaultDamage;


        public override string ToString()
        {
            return $"{_defaultDamage} - {_criticalDamage}";
        }
    }
}