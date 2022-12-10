using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.Units.Stats
{
    [Serializable]
    //Кастомный тип данный описывающий значение любого стата;
    public struct CharacterStatValue
    {
        private const float ConfidenceInterval = 0.001f;
        public static IStatValueProvider CurrentValueProvider { get; private set; } = new GetCurrentValue();

        public static IStatValueProvider MaxValueProvider { get; private set; } = new GetMaxValue();

        public static readonly CharacterStatValue DefaultMax100Current0Min0 = new CharacterStatValue {MinValue = 0, MaxValue = 100, Value = 0};
        public static readonly CharacterStatValue Default000 = new CharacterStatValue {MinValue                = 0, MaxValue = 0, Value   = 0};

        public static CharacterStatValue CreateState(float current, float max, float min = 0)
        {
            return new CharacterStatValue {MinValue = min, MaxValue = max, Value = current};
        }

        public static CharacterStatValue CreateState(float current = 0)
        {
            return new CharacterStatValue {MinValue = current, MaxValue = current, Value = current};
        }


        [HideLabel, HorizontalGroup("Value")] public float MinValue;
        [HorizontalGroup("Value"), SerializeField, PropertyRange("MinValue", "MaxValue"), HideLabel]
        private float _value;
        [HorizontalGroup("Value"), HideLabel] public float MaxValue;

        public float NormalizeValue => Mathf.Clamp(_value, MinValue, MaxValue);

        public float Value
        {
            get => _value;
            set => _value = value;
        }


        public override string ToString()
        {
            return $"{Value:F1} [{MinValue:F1} -> {MaxValue:F1}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is CharacterStatValue statValue)
            {
                return Math.Abs(_value - statValue._value) < ConfidenceInterval &&
                       Math.Abs(MinValue - statValue.MinValue) < ConfidenceInterval &&
                       Math.Abs(MaxValue - statValue.MaxValue) < ConfidenceInterval;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static CharacterStatValue operator +(CharacterStatValue s1, CharacterStatValue s2)
        {
            s1.MinValue += s2.MinValue;
            s1.MaxValue += s2.MaxValue;
            s1.Value    += s2.Value;
            return s1;
        }

        public static CharacterStatValue operator -(CharacterStatValue s1, CharacterStatValue s2)
        {
            s1.MinValue -= s2.MinValue;
            s1.MaxValue -= s2.MaxValue;
            s1.Value    -= s2.Value;
            return s1;
        }

        public static implicit operator float(CharacterStatValue statValue)
        {
            return statValue.Value;
        }
    }
}