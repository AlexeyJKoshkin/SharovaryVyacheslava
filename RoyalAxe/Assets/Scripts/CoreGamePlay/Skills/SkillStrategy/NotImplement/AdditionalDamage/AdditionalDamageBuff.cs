using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelSkill
{
    public abstract class AdditionalDamagePlayerSkill<T> : AbstractPlayerSkillStrategy<T> where T : AdditionalDamageSkillSettings
    {
        protected UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IWeaponItem _additionalDamageApplier;

        class DamageApplier : IWeaponItem
        {
            private AdditionalDamagePlayerSkill<T> _skill;
            private IInfluenceApplier _influenceApplier;

            public DamageApplier(AdditionalDamagePlayerSkill<T> skill, IInfluenceApplier influenceApplier)
            {
                _skill            = skill;
                _influenceApplier = influenceApplier;
            }

            public void AttackTarget(UnitsEntity target)
            {
                _influenceApplier.Apply(_skill.Player, target);
            }

            public float GetSingleValue(DamageType type)
            {
                return _skill.Settings.Damage.ElementalDamage;
            }
        }


        protected AdditionalDamagePlayerSkill(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory) : base(provider)
        {
            _unitsContext = unitsContext;
            var applier = factory.CreateAdditionalDamageApplier(Settings.Damage);
            var damage  = new DamageApplier(this, applier);
            _additionalDamageApplier = damage;
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