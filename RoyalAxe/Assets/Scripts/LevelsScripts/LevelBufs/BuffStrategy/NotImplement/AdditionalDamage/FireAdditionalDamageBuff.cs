using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class FireAdditionalDamageBuff : AbstractBuffStrategy<FiringFirecrackersBuffSettings>
    {
        public FireAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider) : base(provider) { }
    }
}