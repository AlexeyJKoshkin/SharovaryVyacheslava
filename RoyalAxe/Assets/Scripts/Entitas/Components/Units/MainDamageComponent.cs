using Entitas;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas 
{
    /// <summary>
    /// основной урон персонажа
    /// </summary>
    [Units]
    public class MainDamageComponent : IComponent, IInfluenceApplierComposite
    {
        public IInfluenceApplierComposite Influence;
        public void Apply(UnitsEntity attacker, UnitsEntity target)
        {
            Influence.Apply(attacker,target);
        }

        public void IncreaseDamage(DamageType type, float value)
        {
            Influence.IncreaseDamage(type, value);
        }

        public void Upgrade(SkillConfigDef.Damage settingsDamage)
        {
            Influence.Upgrade(settingsDamage);
        }

        public void Downgrade(SkillConfigDef.Damage settingsDamage)
        {
            Influence.Downgrade(settingsDamage);
        }
    }
}