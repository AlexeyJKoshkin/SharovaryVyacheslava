using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamageBuff<T> : AbstractBuffStrategy<T> where T : AdditionalDamageBuffSettings
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IUnitDamageApplierFactory _unitAddDamageToSkillUtility;

        protected AdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitDamageApplierFactory unitAddDamageToSkillUtility, UnitsContext unitsContext) : base(provider)
        {
            _unitAddDamageToSkillUtility = unitAddDamageToSkillUtility;
            _unitsContext             = unitsContext;
        }
        

        public override void DoBuffStrategyActivate()
        {
            var attack = _unitAddDamageToSkillUtility.CreateComposite(Settings.Damage);
            Player.damage.Add(attack);
        }
       
    }
}