using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RoyalAxe.Units.Stats
{
    /// <summary>
    ///     Базовый вообще любой стат игры -стат персонажей, эквипа, активных способностей.
    /// </summary>
    [DontGenerate]
    public abstract class ModifiableStat : AbstractStat, IGameStatChangeable, IComponent
    {
        private class StatModificator : IChangeModificatorBuilder
        {
            private readonly IStatValueProvider _provider;

            private readonly ModifiableStat _modifiableStat;
            public CharacterStatValue ModValue { get; private set; }

            public bool RemoveMode()
            {
                return _modifiableStat.RemoveMod(this);
            }

            public IGameStat Stat => _modifiableStat;

            public StatModificator(IStatValueProvider provider, ModifiableStat modifiableStat)
            {
                _provider       = provider;
                _modifiableStat = modifiableStat;
                ModValue        = CharacterStatValue.Default000;
            }

            IChangeModificatorBuilder IChangeModificatorBuilder.ByConstValue(float constValue)
            {
                var modvalue = _provider.SetValue(constValue);
                ModValue = modvalue + ModValue;
                return this;
            }


            public IChangeModificatorBuilder CalculatePercent(CharacterStatValue source, IStatValueProvider currentValueProvider, float percent)
            {
                var oldValue = currentValueProvider.GetValue(source);
                var newValue = oldValue * percent / 100;
                ModValue = ModValue + _provider.SetValue(newValue);
                return this;
            }


            ICharacterStatModificator IModApplier.ApplyMod()
            {
                _modifiableStat.ApplyMod(this);
                return this;
            }

            ICharacterStatModificator IModApplier.ApplyPermanentMod()
            {
                _modifiableStat.ApplyPermanentMod(this);
                return this;
            }

            IChangeModificatorBuilder IChangeModificatorBuilder.FromNativeCurrent(float percent)
            {
                return CalculatePercent(_modifiableStat._nativeStat, CharacterStatValue.CurrentValueProvider, percent);
            }

            IChangeModificatorBuilder IChangeModificatorBuilder.FromActualCurrent(float percent)
            {
                return CalculatePercent(_modifiableStat._actualStat, CharacterStatValue.CurrentValueProvider, percent);
            }

            IChangeModificatorBuilder IChangeModificatorBuilder.FromNativeMax(float percent)
            {
                return CalculatePercent(_modifiableStat._nativeStat, CharacterStatValue.MaxValueProvider, percent);
            }

            IChangeModificatorBuilder IChangeModificatorBuilder.FromActualMax(float percent)
            {
                return CalculatePercent(_modifiableStat._actualStat, CharacterStatValue.MaxValueProvider, percent);
            }

            IChangeModificatorBuilder IChangeModificatorBuilder.FromNative(ModificatorChangeValueType percentRefType, float percent)
            {
                switch (percentRefType)
                {
                    case ModificatorChangeValueType.Current:  return ((IChangeModificatorBuilder) this).FromNativeCurrent(percent);
                    case ModificatorChangeValueType.MaxValue: return ((IChangeModificatorBuilder) this).FromNativeMax(percent);
                    default:                                  throw new ArgumentOutOfRangeException(nameof(percentRefType), percentRefType, null);
                }
            }

            IChangeModificatorBuilder IChangeModificatorBuilder.FromActual(ModificatorChangeValueType percentRefType, float percent)
            {
                switch (percentRefType)
                {
                    case ModificatorChangeValueType.Current:  return ((IChangeModificatorBuilder) this).FromActualCurrent(percent);
                    case ModificatorChangeValueType.MaxValue: return ((IChangeModificatorBuilder) this).FromActualMax(percent);
                    default:                                  throw new ArgumentOutOfRangeException(nameof(percentRefType), percentRefType, null);
                }
            }
        }

        public CharacterStatValue UnitStatValue
        {
            get => _actualStat;
            set
            {
                _actualStat = value;
                _nativeStat = value;
            }
        }
        
        public CharacterStatValue NativeStatValue => _nativeStat;

        public sealed override float CurrentValue => _actualStat.NormalizeValue;
        public sealed override float MaxValue => _actualStat.MaxValue;
        public sealed override float MinValue => _actualStat.MinValue;

        private CharacterStatValue _actualStat;
        private CharacterStatValue _nativeStat;
        private readonly HashSet<ICharacterStatModificator> _modificators = new HashSet<ICharacterStatModificator>();

        public IChangeModificatorBuilder Change(ModificatorChangeValueType modificatorChangeValueType)
        {
            switch (modificatorChangeValueType)
            {
                case ModificatorChangeValueType.Current:
                    return ChangeValue();
                case ModificatorChangeValueType.MaxValue: return ChangeMaxValue();
                default:                                  throw new ArgumentOutOfRangeException(nameof(modificatorChangeValueType), modificatorChangeValueType, null);
            }
        }

        public IChangeModificatorBuilder ChangeValue()
        {
            return new StatModificator(CharacterStatValue.CurrentValueProvider, this);
        }

        public IChangeModificatorBuilder ChangeValue(float delta)
        {
            IChangeModificatorBuilder builder = ChangeValue();
            return builder.ByConstValue(delta);
        }

        public IChangeModificatorBuilder ChangeMaxValue()
        {
            return new StatModificator(CharacterStatValue.MaxValueProvider, this);
        }

        public IChangeModificatorBuilder ChangeMaxValue(float delta)
        {
            IChangeModificatorBuilder builder = ChangeMaxValue();
            return builder.ByConstValue(delta);
        }

        public bool RemoveMod(ICharacterStatModificator modificator)
        {
            if (!_modificators.Contains(modificator))
            {
                return false;
            }

            _actualStat -= modificator.ModValue;
            _modificators.Remove(modificator);
            return true;
        }

        public override void Reset()
        {
            _actualStat = _nativeStat;
            _modificators.Clear();
        }

        public void ApplyMod(ICharacterStatModificator modificator)
        {
            if (modificator == default)
            {
                return;
            }

            if (_modificators.Contains(modificator))
            {
                return;
            }

            _actualStat += modificator.ModValue;
            _modificators.Add(modificator);
        }

        public void ApplyPermanentMod(ICharacterStatModificator modificator)
        {
            if (modificator == default)
            {
                return;
            }

            _actualStat += modificator.ModValue;
            _nativeStat += modificator.ModValue;
        }
    }
}