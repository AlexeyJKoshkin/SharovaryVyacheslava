using NUnit.Framework;
using RoyalAxe.CharacterStat;

public class StatModificatorTest
{
    private CharacterStatValue _originStat = new CharacterStatValue
    {
        MinValue = 0,
        MaxValue = 100,
        Value    = 50
    };

    [TestCase(-10f), TestCase(0), TestCase(-10)]
    public void CheckCalculateConstChangeValue(float constValue)
    {
        CheckConstCurrent(constValue);
        CheckConstMax(constValue);
    }

    private void CheckConstMax(float constValue)
    {
        ModifiableStat stat  = new ModifiableStateMock(_originStat);
        var            mod   = stat.ChangeMaxValue(constValue);
        var            delta = mod.ModValue;
        Check(delta, new CharacterStatValue
        {
            MaxValue = constValue
        });
    }

    private void CheckConstCurrent(float constValue)
    {
        ModifiableStat stat  = new ModifiableStateMock(_originStat);
        var            mod   = stat.ChangeValue(constValue);
        var            delta = mod.ModValue;

        Check(delta, new CharacterStatValue
        {
            Value = constValue
        });
    }


    [TestCase(-10f), TestCase(0), TestCase(-10)]
    public void CheckPercentChangeFromCurrent(float percent)
    {
        CheckCurrentPercentChangeFromCurrent(percent);
        CheckMaxPercentChangeFromCurrent(percent);
    }

    private void CheckMaxPercentChangeFromCurrent(float percent)
    {
        ModifiableStat stat  = new ModifiableStateMock(_originStat);
        var            mod   = stat.ChangeMaxValue().FromActualCurrent(percent);
        var            delta = mod.ModValue;

        var expected = new CharacterStatValue
        {
            MinValue = 0,
            MaxValue = _originStat.Value * percent / 100,
            Value    = 0
        };
        Check(delta, expected);
    }

    private void CheckCurrentPercentChangeFromCurrent(float percent)
    {
        ModifiableStat stat  = new ModifiableStateMock(_originStat);
        var            mod   = stat.ChangeValue().FromActualCurrent(percent);
        var            delta = mod.ModValue;

        var expected = new CharacterStatValue
        {
            MinValue = 0,
            MaxValue = 0,
            Value    = _originStat.Value * percent / 100
        };
        Check(delta, expected);
    }

    [TestCase(-10f), TestCase(0), TestCase(-10)]
    public void CheckPercentChangeFromMax(float constValue)
    {
        CheckCurrentPercentChangeFromMax(constValue);
        CheckMaxPercentChangeFromMax(constValue);
    }

    private void CheckMaxPercentChangeFromMax(float percent)
    {
        ModifiableStat stat  = new ModifiableStateMock(_originStat);
        var            mod   = stat.ChangeMaxValue().FromActualMax(percent);
        var            delta = mod.ModValue;

        var expected = new CharacterStatValue
        {
            MinValue = 0,
            MaxValue = _originStat.MaxValue * percent / 100,
            Value    = 0
        };
        Check(delta, expected);
    }

    private void CheckCurrentPercentChangeFromMax(float percent)
    {
        ModifiableStat stat  = new ModifiableStateMock(_originStat);
        var            mod   = stat.ChangeValue().FromActualMax(percent);
        var            delta = mod.ModValue;

        var expected = new CharacterStatValue
        {
            MinValue = 0,
            MaxValue = 0,
            Value    = _originStat.MaxValue * percent / 100
        };
        Check(delta, expected);
    }

    private void Check(CharacterStatValue delta, CharacterStatValue expectedResult)
    {
        Assert.AreEqual(expectedResult.Value, delta.Value, "Current value error");
        Assert.AreEqual(expectedResult.MaxValue, delta.MaxValue, "Max value error");
        Assert.AreEqual(expectedResult.MinValue, delta.MinValue, "Min value error");
    }
}