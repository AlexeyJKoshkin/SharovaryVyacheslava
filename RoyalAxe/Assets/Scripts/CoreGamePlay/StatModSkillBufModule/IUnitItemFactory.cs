using RoyalAxe.GameEntitas;

namespace RoyalAxe.CharacterStat 
{
    public interface IUnitItemFactory
    {
        MainWeaponItem CreateMainItem(params SkillConfigDef.Damage[] damageData);
    }
}