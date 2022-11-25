using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamageBuff<T> : AbstractBuffStrategy<T> where T : AdditionalDamageBuffSettings
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IUnitAddDamageToSkillUtility _unitAddDamageToSkillUtility;

        protected AdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitAddDamageToSkillUtility unitAddDamageToSkillUtility, UnitsContext unitsContext) : base(provider)
        {
            _unitAddDamageToSkillUtility = unitAddDamageToSkillUtility;
            _unitsContext             = unitsContext;
        }
        

        public override void DoBuffStrategyActivate()
        {
            _unitAddDamageToSkillUtility.AddDamageToUnit(Player.damage, Settings.Damage);
            /*var damage = Player.damage.SingleDamage.FirstOrDefault(o => o.Type == Settings.DamageTypeType) ?? CreateSingleDamage();
            if (Settings.PercentActiveDamage > 0)
            {
                var maxPhysDamage = damageComponent.SingleDamage.Where(o => o.Type == DamageType.Physical).Max(o => o.Value);
                damage.AddDamage(maxPhysDamage * Settings.PercentActiveDamage * .01f);
            }*/
        
        }
       
    }
}