using System;
using System.Collections.Generic;
using Core.Data.Provider;
using RoyalAxe.CoreLevel;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.Configs 
{
    public abstract class UnitJsonData : IDataObject 
    {
        public string UniqueID { get; set; }
        public List<StatsConfig> StatCollection = new List<StatsConfig>();

      
    }

    [Serializable]
    public class HeroUnitJsonData : UnitJsonData
    {
        
    }

    [Serializable]
    public class MobUnitJsonData : UnitJsonData 
    {
        public MobWeaponSkillConfigDef MobWeaponData;
        public List<MobDeathReward> MobDeathReward = new List<MobDeathReward>();
    }

    public static class UnitJsonExtension
    {
        public static StatsConfig GetStatByLevel(this UnitJsonData unit,int lvl)
        {
            return GetLevelParam(unit.StatCollection, lvl);
        }
        
        public static MobDeathReward GetDeathRewardByLevel(this MobUnitJsonData unit,int lvl)
        {
            return GetLevelParam(unit.MobDeathReward, lvl);
        }

        static T GetLevelParam<T>(List<T> unitLevelParams, int lvl) where T : new()
        {
            lvl--; // уровнь всегда на 1 больше чем индекс
            if (lvl < unitLevelParams.Count)
            {
                return unitLevelParams[lvl];
            }

            return new T();
        }
    }
}