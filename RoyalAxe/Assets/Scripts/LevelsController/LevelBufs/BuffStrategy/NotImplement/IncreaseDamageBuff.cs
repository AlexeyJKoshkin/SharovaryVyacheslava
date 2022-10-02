using System.Linq;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public class IncreaseDamageBuff : AbstractBuffStrategy
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly IncreaseDamageBuffSettings _settings;
        private readonly UnitsContext _unitsContext;

        public IncreaseDamageBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider)
        {
            _unitsContext = unitsContext;
            _settings     = provider.SettingsComposite.IncreaseDamageBuffSetting;
        }

        public override void Activate()
        {
            UnitsEntity player = null;
            //либо увеличиваем физ дамаг как стат
           // player.physicalDamage.ChangeValue(_increaseDamage).ApplyPermanentMod();
            //либо увеличиваем физ дамаг в способности
          //  player.defaultDamage.Damage.PhysicalDamage += _increaseDamage;
            
            var damageComponent = Player.damage;

            var physDamage = damageComponent.SingleDamage.FirstOrDefault(o => o.Type == DamageType.Physical);

            if (physDamage == null)
            {
                damageComponent.SingleDamage.Add(new OneMomentDamageOperation(DamageType.Physical, _settings.Value));
            }
            else
                physDamage.AddDamage(_settings.Value);
        }
    }
}