using System;
using System.Collections;
using System.Linq;
using GameKit;
using RoyalAxe.CharacterStat;

namespace RoyalAxe
{

    public interface IInfluenceApplierComposite : IInfluenceApplier
    {
        //Усиливаем урон от урона на абсолютную величину
        void IncreaseDamage(DamageType physical, float settingsValue);
        void Upgrade(SkillConfigDef.Damage settingsDamage);
        void Downgrade(SkillConfigDef.Damage settingsDamage);
    }

    public interface IInfluenceApplier
    {
        void Apply(UnitsEntity attacker, UnitsEntity target);
    }


    public interface IPeriodicInfluenceApplier : IInfluenceApplier
    {
        SkillConfigDef.Damage DamageData { get; }
    }
}