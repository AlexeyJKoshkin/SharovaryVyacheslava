using System.Linq;
using System.Runtime.InteropServices;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamageBuff : AbstractBuffStrategy
    {
        public override bool IsSingle => true;
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;
        private readonly AdditionDamageBuffSettings _settings;


        public AdditionalDamageBuff(UnitsContext unitsContext,
                                    IUnitDamageApplierFactory unitDamageApplierFactory,
                                    ILevelBuffSettingCompositeProvider provider)
        {
            _unitsContext = unitsContext;
            _unitDamageApplierFactory = unitDamageApplierFactory;
            _settings     = GetSettings(provider.SettingsComposite);
        }

        protected abstract AdditionDamageBuffSettings GetSettings(LevelBuffSettingsComposite providerSettingsComposite);

        public override void Activate()
        {
            var damageComponent = Player.damage;

            var maxPhysDamage = damageComponent.SingleDamage.Where(o => o.Type == DamageType.Physical).Max(o => o.Value);

            var elementalDamage = new DamageInfluenceData()
            {
                ElementalDamageType = _settings.Type,
                Damage = maxPhysDamage * _settings.PercentActiveDamage*.01f
            };

            var damage = _unitDamageApplierFactory.CreateOneMomentDamage(_settings.Type, maxPhysDamage * _settings.PercentActiveDamage * .01f);
            Player.damage.SingleDamage.Add(damage); 
            
        }
    }
}