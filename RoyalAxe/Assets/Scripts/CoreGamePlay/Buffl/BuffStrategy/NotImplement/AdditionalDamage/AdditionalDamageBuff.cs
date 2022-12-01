using System.Collections.Generic;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamagePower<T> : AbstractPowerStrategyStrategy<T> where T : AdditionalDamageBuffSettings
    {
        protected UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;

        protected AdditionalDamagePower(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider)
        {
            _unitsContext = unitsContext;
        }

        protected void AddOtherDamage(IInfluenceApplier applier)
        {
            var list = this.Player.hasOtherDamage ? this.Player.otherDamage.Collection : new List<IInfluenceApplier>();
            list.Add(applier); // добавили самого себя, в качестве сущности, которая будет навешивать баф/заморозку

            this.Player.ReplaceOtherDamage(list); // обновили сущность 
        }
        
        public override void DoLevelPowerActivate()
        {
            Player.mainDamage.Upgrade(Settings.Damage);
        }

        public override void DoLevelPowerDeActivate()
        {
            Player.mainDamage.Downgrade(Settings.Damage);
        }
    }
}