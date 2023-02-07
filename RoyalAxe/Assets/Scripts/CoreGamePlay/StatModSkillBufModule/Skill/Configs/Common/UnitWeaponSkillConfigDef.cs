using System;
using Core.Data.Provider;

namespace RoyalAxe.Units.Stats {
    [Serializable]
    public class UnitWeaponSkillConfigDef : SkillConfigDef,IDataObject 
    {
        public string UniqueID { get; set; }
        
        public UnitWeaponSkillConfigDef()
        {
        }
        
        public UnitWeaponSkillConfigDef(int cellsCount) :base(cellsCount)
        {
        }
    }
}