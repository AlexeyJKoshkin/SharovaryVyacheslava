using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamageBuff<T> : AbstractBuffStrategy<T> where T : AdditionalDamageBuffSettings
    {
        protected UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IUnitDamageApplierFactory _unitAddDamageToSkillUtility;

        protected AdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitDamageApplierFactory unitAddDamageToSkillUtility, UnitsContext unitsContext) : base(provider)
        {
            _unitAddDamageToSkillUtility = unitAddDamageToSkillUtility;
            _unitsContext             = unitsContext;
        }
        

        public override void DoBuffStrategyActivate()
        {
            Player.damage.MainInfluence.IncreaseDamage(Settings.DamageTypeType, Settings.Damage.ElementalDamage);
        }
       
    }
}