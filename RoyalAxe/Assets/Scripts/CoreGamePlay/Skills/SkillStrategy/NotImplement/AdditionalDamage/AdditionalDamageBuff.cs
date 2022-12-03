using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamagePower<T> : AbstractPowerStrategyStrategy<T> where T : AdditionalDamageSkillSettings
    {
        protected UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IInfluenceApplier _additionalDamageApplier;
        
        

        protected AdditionalDamagePower(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory) : base(provider)
        {
            _unitsContext = unitsContext;
            _additionalDamageApplier = factory.CreateAdditionalDamageApplier(Settings.Damage);
        }

       
        
        public override void DoLevelPowerActivate()
        {
            this.Player.AddMoreDamage(_additionalDamageApplier);
        }

        public override void DoLevelPowerDeActivate()
        {
            Player.otherDamage.Remove(_additionalDamageApplier);
            Player.ReplaceOtherDamage(Player.otherDamage.Collection);
        }
    
    }
}