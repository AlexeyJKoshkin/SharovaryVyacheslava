using UnityEngine;

namespace RoyalAxe {
    class SpanSingleDamageValue : IDamageValue
    {
        private float _minValue;
        private float _maxValue;
        private float _crit;

        public SpanSingleDamageValue(float minValue, float maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public float Damage => Random.Range(0f, 1f) < 0.5 ? _minValue : _maxValue;
        public float CriticalDamage
        {
            get => _crit;
            set { _crit = Mathf.Max(0, value); }
        }

        public void IncreaseDamage(float delta)
        {
            _minValue = Mathf.Max(0, _minValue + delta);
            _maxValue = Mathf.Max(0, _maxValue + delta);
        }
    }
}