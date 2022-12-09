using RoyalAxe.GameEntitas;

namespace RoyalAxe.CharacterStat 
{
    public class UnitItemFactory : IUnitItemFactory
    {
        private readonly IUnitDamageApplierFactory _factory;
        public UnitItemFactory(IUnitDamageApplierFactory factory)
        {
            _factory = factory;
        }


        public MainWeaponItem CreateMainItem(params SkillConfigDef.Damage[] damageData)
        {
            var composite      = _factory.CreateComposite();
            var mainWeaponItem = new MainWeaponItem(composite,_factory);
            
            if (damageData == null || damageData.Length == 0) return mainWeaponItem;
            
            for (int i = 0; i < damageData.Length; i++)
            {
                mainWeaponItem.IncreaseDamage(damageData[i]);
            }

            return mainWeaponItem;
        }
    }
}