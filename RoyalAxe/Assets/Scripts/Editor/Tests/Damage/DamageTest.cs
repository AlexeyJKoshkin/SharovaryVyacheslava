using System.Collections.Generic;
using NUnit.Framework;
using RoyalAxe.CharacterStat;

public class DamageUnitTest
{
    private UnitsContext _unitsContext;

    [SetUp]
    public void SetUpTest()
    {
        _unitsContext = new UnitsContext();
    }

    [Test]
    public void TestEquipWeapon() { }

    private UnitsEntity CreateTestUnit(int health, string id)
    {
        var result = _unitsContext.CreateEntity();


        /*result.AddHealth(new ModifiableStat(new CharacterStatValue()
        {
            MinValue = 0,
            MaxValue = 100,
            Value = health
        }));*/

        result.AddActiveUnitBuff(new HashSet<IEntityBuff>());
        return result;
    }

    [Test]
    public void Test2() { }
}