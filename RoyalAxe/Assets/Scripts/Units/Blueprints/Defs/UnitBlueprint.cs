using Core.UserProfile;
using RoyalAxe.Configs;

namespace RoyalAxe.GameEntitas {
    public class UnitBlueprint : BaseBlueprint
    {
        public StatsConfig Stats;
        public WeaponBluePrint MainItemBluePrint;

        public UnitBlueprint(SaveEntityRecord saveRecord):this(saveRecord.Id, saveRecord.Level)
        {
        }

        public UnitBlueprint(string id, int level) : base(id, level)
        {
        }
    }
}