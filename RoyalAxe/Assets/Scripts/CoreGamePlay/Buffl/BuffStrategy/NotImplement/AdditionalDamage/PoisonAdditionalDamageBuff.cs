
namespace RoyalAxe.LevelBuff {
    public class PoisonAdditionalDamagePower : AdditionalDamagePower<PoisonAdditionalDamageBuffSettings>
    {
        public PoisonAdditionalDamagePower(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider, unitsContext) { }
      
    }
}