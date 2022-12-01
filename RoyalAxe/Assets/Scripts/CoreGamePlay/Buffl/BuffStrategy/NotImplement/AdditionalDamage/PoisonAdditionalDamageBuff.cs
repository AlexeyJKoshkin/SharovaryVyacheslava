
namespace RoyalAxe.LevelBuff {
    public class PoisonAdditionalDamageBuff : AdditionalDamageBuff<PoisonAdditionalDamageBuffSettings>
    {
        public PoisonAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider, unitsContext) { }
    }
}