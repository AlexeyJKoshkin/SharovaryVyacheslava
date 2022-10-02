using System;
using NUnit.Framework;
using RoyalAxe.CharacterStat;

public class ModifiableStateMock : ModifiableStat
{
    public ModifiableStateMock(CharacterStatValue stat)
    {
        UnitStatValue = stat;
    }
}

public class ModifiableStatTest
{
    private ModifiableStat _originstat;
    private readonly CharacterStatValue _defaultStatValue = new CharacterStatValue
    {
        MinValue = -100,
        Value    = 50,
        MaxValue = 100
    };

    [SetUp]
    public void SetUp()
    {
        _originstat = new ModifiableStateMock(_defaultStatValue);
    }

    [TestCase(-10, ModificatorChangeValueType.Current), TestCase(0, ModificatorChangeValueType.Current), TestCase(10, ModificatorChangeValueType.Current), TestCase(-10, ModificatorChangeValueType.MaxValue), TestCase(0, ModificatorChangeValueType.MaxValue), TestCase(10, ModificatorChangeValueType.MaxValue)]
    public void TestAddRemoveConstMod(float constValue, ModificatorChangeValueType changeValueType)
    {
        var mod = _originstat.Change(changeValueType).ByConstValue(constValue).ApplyMod();

        var expectedStat = _defaultStatValue;

        switch (changeValueType)
        {
            case ModificatorChangeValueType.Current:
                expectedStat.Value += constValue;
                break;
            case ModificatorChangeValueType.MaxValue:
                expectedStat.MaxValue += constValue;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(changeValueType), changeValueType, null);
        }

        Check(_originstat, expectedStat);

        _originstat.RemoveMod(mod);
        Check(_originstat, _defaultStatValue);
    }

    [TestCase(-10, ModificatorChangeValueType.Current), TestCase(0, ModificatorChangeValueType.Current), TestCase(10, ModificatorChangeValueType.Current), TestCase(-10, ModificatorChangeValueType.MaxValue), TestCase(0, ModificatorChangeValueType.MaxValue), TestCase(10, ModificatorChangeValueType.MaxValue)]
    public void TestOneAddRemovePercentModFromMax(float percent, ModificatorChangeValueType changeValueType)
    {
        var mod = _originstat.Change(changeValueType).FromNativeMax(percent).ApplyMod();

        var expectedStat = _defaultStatValue;

        switch (changeValueType)
        {
            case ModificatorChangeValueType.Current:
                expectedStat.Value += expectedStat.MaxValue * percent / 100;
                break;
            case ModificatorChangeValueType.MaxValue:
                expectedStat.MaxValue += expectedStat.MaxValue * percent / 100;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(changeValueType), changeValueType, null);
        }

        Check(_originstat, expectedStat);

        _originstat.RemoveMod(mod);
        Check(_originstat, _defaultStatValue);
    }

    [TestCase(-10, ModificatorChangeValueType.Current), TestCase(0, ModificatorChangeValueType.Current), TestCase(10, ModificatorChangeValueType.Current), TestCase(-10, ModificatorChangeValueType.MaxValue), TestCase(0, ModificatorChangeValueType.MaxValue), TestCase(10, ModificatorChangeValueType.MaxValue)]
    public void TestOneAddRemovePercentModFromActual(float percent, ModificatorChangeValueType changeValueType)
    {
        var mod = _originstat.Change(changeValueType).FromActualCurrent(percent);
        _originstat.ApplyMod(mod);

        var expectedStat = _defaultStatValue;

        switch (changeValueType)
        {
            case ModificatorChangeValueType.Current:
                expectedStat.Value += expectedStat.Value * percent / 100;
                break;
            case ModificatorChangeValueType.MaxValue:
                expectedStat.MaxValue += expectedStat.Value * percent / 100;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(changeValueType), changeValueType, null);
        }

        Check(_originstat, expectedStat);

        _originstat.RemoveMod(mod);
        Check(_originstat, _defaultStatValue);
    }

    [TestCase(-10, ModificatorChangeValueType.Current), TestCase(0, ModificatorChangeValueType.Current), TestCase(10, ModificatorChangeValueType.Current), TestCase(-10, ModificatorChangeValueType.MaxValue), TestCase(0, ModificatorChangeValueType.MaxValue), TestCase(10, ModificatorChangeValueType.MaxValue)]
    public void TestSeveralAddPercentModFromMax(float percent, ModificatorChangeValueType changeValueType)
    {
        var expectedStat = _defaultStatValue;

        for (int i = 0; i < 3; i++)
        {
            var mod = _originstat.Change(changeValueType).FromActualMax(percent);
            _originstat.ApplyMod(mod);

            switch (changeValueType)
            {
                case ModificatorChangeValueType.Current:
                    expectedStat.Value += expectedStat.MaxValue * percent / 100;
                    break;
                case ModificatorChangeValueType.MaxValue:
                    expectedStat.MaxValue += expectedStat.MaxValue * percent / 100;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(changeValueType), changeValueType, null);
            }

            Check(_originstat, expectedStat);
        }

        _originstat.Reset();
        Check(_originstat, _defaultStatValue);
    }

    [TestCase(-10, ModificatorChangeValueType.Current), TestCase(0, ModificatorChangeValueType.Current), TestCase(10, ModificatorChangeValueType.Current), TestCase(-10, ModificatorChangeValueType.MaxValue), TestCase(0, ModificatorChangeValueType.MaxValue), TestCase(10, ModificatorChangeValueType.MaxValue)]
    public void TestSeveralAddPercentModFromCurrent(float percent, ModificatorChangeValueType changeValueType)
    {
        var expectedStat = _defaultStatValue;

        for (int i = 0; i < 3; i++)
        {
            var mod = _originstat.Change(changeValueType).FromActualCurrent(percent);
            _originstat.ApplyMod(mod);

            switch (changeValueType)
            {
                case ModificatorChangeValueType.Current:
                    expectedStat.Value += expectedStat.Value * percent / 100;
                    break;
                case ModificatorChangeValueType.MaxValue:
                    expectedStat.MaxValue += expectedStat.Value * percent / 100;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(changeValueType), changeValueType, null);
            }

            Check(_originstat, expectedStat);
        }

        _originstat.Reset();
        Check(_originstat, _defaultStatValue);
    }

    private void Check(ModifiableStat stat, CharacterStatValue expectedResult)
    {
        Assert.AreEqual(expectedResult.Value, stat.CurrentValue, "Current value error");
        Assert.AreEqual(expectedResult.MaxValue, stat.MaxValue, "Max value error");
        Assert.AreEqual(expectedResult.MinValue, stat.MinValue, "Min value error");
        Assert.IsTrue(stat.CurrentValue <= stat.MaxValue, "Текущий стат больше максимального");
        Assert.IsTrue(stat.CurrentValue >= stat.MinValue, "Текущий стат меньше минимального");
    }
}