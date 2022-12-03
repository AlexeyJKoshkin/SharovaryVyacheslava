using System;
using System.Collections.Generic;
using GameKit;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelBuff;
using UnityEngine;
using VContainer;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/LevelBuffsInstaller", fileName = "LevelBuffsInstaller")]
    public class LevelBuffsInstaller : ScriptableInstaller
    {
        protected override void InstallBindings()
        {
            Container.Register<LevelSkillStorage>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<CurrentPlayerSkillDistributor>(Lifetime.Singleton).AsImplementedInterfaces();

            AllGameBuffs().ForEach(t => { Container.Register(t, Lifetime.Singleton).As<ILevelSkill>(); });

         
        }

        IEnumerable<Type> AllGameBuffs()
        {
            yield return typeof(RoyalAxe.LevelBuff.FiringBladePower);
            yield return typeof(RoyalAxe.LevelBuff.FireAdditionalDamagePower);
            yield return typeof(RoyalAxe.LevelBuff.ColdAdditionalDamagePower);
            yield return typeof(RoyalAxe.LevelBuff.PoisonAdditionalDamagePower);
          //  yield return typeof(RoyalAxe.LevelBuff.FiringFirecrackersBuff);
          //  yield return typeof(RoyalAxe.LevelBuff.FloatingShieldsBuff);
            yield return typeof(RoyalAxe.LevelBuff.HealPlayerLifePower);
            yield return typeof(RoyalAxe.LevelBuff.IncreaseCriticalChancePower);
            yield return typeof(RoyalAxe.LevelBuff.IncreaseDamagePower);
            yield return typeof(RoyalAxe.LevelBuff.IncreasePlayerMaxLifePower);
            yield return typeof(RoyalAxe.LevelBuff.IncreasePlayerSkillSpeedPower);
            yield return typeof(RoyalAxe.LevelBuff.IncreasePlayerSkillUsagePower);
          //  yield return typeof(RoyalAxe.LevelBuff.InfectedBloodBuff);
           // yield return typeof(RoyalAxe.LevelBuff.RicochetBuff);
            yield return typeof(RoyalAxe.LevelBuff.SequentialWeaponRollPower);
          //  yield return typeof(RoyalAxe.LevelBuff.ThroughDamageBuff);
            yield return typeof(RoyalAxe.LevelBuff.TripleWeaponRollPower);
        }
    }
}