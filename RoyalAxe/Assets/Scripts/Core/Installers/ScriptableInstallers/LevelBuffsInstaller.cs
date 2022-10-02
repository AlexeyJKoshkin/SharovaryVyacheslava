using System;
using System.Collections.Generic;
using GameKit;
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
            Container.Register<LevelBuffStorage>(Lifetime.Singleton).AsImplementedInterfaces();
            
            AllGameBuffs().ForEach(t => { Container.Register(t, Lifetime.Singleton).As<ILevelBuff>(); });
        }

        IEnumerable<Type> AllGameBuffs()
        {
           // yield return typeof(RoyalAxe.LevelBuff.DoubleParallelWeaponThrowBuff);
           // yield return typeof(RoyalAxe.LevelBuff.FiringBladeBuff);
           // yield return typeof(RoyalAxe.LevelBuff.FiringFirecrackersBuff);
           // yield return typeof(RoyalAxe.LevelBuff.FloatingShieldsBuff);
            /*yield return typeof(RoyalAxe.LevelBuff.HealPlayerLifeBuff);
            yield return typeof(RoyalAxe.LevelBuff.IncreaseCriticalChanceBuff);
            yield return typeof(RoyalAxe.LevelBuff.IncreaseDamageBuff);
            yield return typeof(RoyalAxe.LevelBuff.IncreasePlayerMaxLifeBuff);
            yield return typeof(RoyalAxe.LevelBuff.IncreasePlayerSkillSpeedBuff);
            yield return typeof(RoyalAxe.LevelBuff.IncreasePlayerSkillUsageBuff);*/
          //  yield return typeof(RoyalAxe.LevelBuff.InfectedBloodBuff);
          //  yield return typeof(RoyalAxe.LevelBuff.RicochetBuff);
          //  yield return typeof(RoyalAxe.LevelBuff.SequentialWeaponRollBuff);
          //  yield return typeof(RoyalAxe.LevelBuff.ThroughDamageBuff);
            yield return typeof(RoyalAxe.LevelBuff.TripleWeaponRollBuff);
        }
    }
}