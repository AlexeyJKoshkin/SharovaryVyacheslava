using System;
using Core.Data.Provider;

namespace RoyalAxe.Units.Stats {
    [Serializable]
    public class UnitWeaponSkillConfigDef : SkillConfigDef,IDataObject 
    {
        public UnitWeaponSkillConfigDef()
        {
        }
        
        public UnitWeaponSkillConfigDef(string pagePageName, int cellsCount) :base(pagePageName, cellsCount)
        {
        }
    }
}