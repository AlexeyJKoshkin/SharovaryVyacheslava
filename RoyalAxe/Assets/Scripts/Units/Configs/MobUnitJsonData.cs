using System.Collections.Generic;
using Core.Data.Provider;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.Configs 
{
    public abstract class UnitJsonData : IDataObject 
    {
        public string UniqueID { get; set; }
        public List<StatsConfig> StatCollection = new List<StatsConfig>();

        public StatsConfig GetStatByLevel(int lvl)
        {
            lvl--; // уровнь всегда на 1 больше чем индекс
            if (lvl < StatCollection.Count)
            {
                return StatCollection[lvl];
            }

            return new StatsConfig();
        }
    }

    public class HeroUnitJsonData : UnitJsonData
    {
        
    }

    public class MobUnitJsonData : UnitJsonData 
    {
        public MobWeaponSkillConfigDef MobWeaponData;
    }
}