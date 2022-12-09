using System;
using System.Collections.Generic;
using GameKit;
using RoyalAxe.LevelSkill;
using UnityEngine;
using VContainer;

namespace Core {
    [CreateAssetMenu(menuName = "Installers/LevelSkillsInstaller", fileName = "LevelSkillsInstaller")]
    public class LevelSkillsInstaller : ScriptableInstaller
    {
        protected override void InstallBindings()
        {
            Container.Register<LevelSkillStorage>(Lifetime.Singleton).As<ILevelSkillStorage>();
            Container.Register<CurrentPlayerSkillDistributor>(Lifetime.Singleton).AsImplementedInterfaces();

            AllGameBuffs().ForEach(t => { Container.Register(t, Lifetime.Singleton).As<ILevelSkill>(); });

         
        }

        IEnumerable<Type> AllGameBuffs()
        {
            yield return typeof(RoyalAxe.LevelSkill.FiringBladePlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.FireAdditionalDamagePlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.ColdAdditionalDamagePlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.PoisonAdditionalDamagePlayerSkill);
            //  yield return typeof(RoyalAxe.LevelBuff.FiringFirecrackersBuff);
            //  yield return typeof(RoyalAxe.LevelBuff.FloatingShieldsBuff);
            yield return typeof(RoyalAxe.LevelSkill.HealPlayerLifePlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.IncreaseCriticalChancePlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.IncreaseDamagePlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.IncreasePlayerMaxLifePlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.IncreasePlayerSkillSpeedPlayerSkill);
            yield return typeof(RoyalAxe.LevelSkill.IncreasePlayerSkillUsagePlayerSkill);
            //  yield return typeof(RoyalAxe.LevelBuff.InfectedBloodBuff);
            // yield return typeof(RoyalAxe.LevelBuff.RicochetBuff);
            yield return typeof(RoyalAxe.LevelSkill.SequentialWeaponRollPlayerSkill);
            //  yield return typeof(RoyalAxe.LevelBuff.ThroughDamageBuff);
            yield return typeof(RoyalAxe.LevelSkill.TripleWeaponRollPlayerSkill);
        }
    }
}