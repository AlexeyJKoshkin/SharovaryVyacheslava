using RoyalAxe.GameEntitas;

namespace RoyalAxe.Units.Stats 
{
    public class UnitItemFactory : IUnitItemFactory
    {
        private readonly IUnitDamageApplierFactory _factory;

        public UnitItemFactory(IUnitDamageApplierFactory factory)
        {
            _factory = factory;
        }


        // создаем основное оружие из урона
        public MainWeaponItem CreateMainItem(WeaponBluePrint weaponConfig)
        {
            var composite      = _factory.CreateComposite();
            var mainWeaponItem = new MainWeaponItem(composite,_factory);
            AddDamage(mainWeaponItem,weaponConfig.SkillBlueprint.DamageData);
            // тут же надо фигачить все IUnitApplierItem, на которые способно это оружие добавить скилы/увеличить статы/навесить бафы
            return mainWeaponItem;
        }
        
        static void AddDamage(MainWeaponItem weapon, params SkillConfigDef.Damage[] damageData)
        {
            if(weapon == null) return;
            if (damageData == null || damageData.Length == 0) return;
            
            for (int i = 0; i < damageData.Length; i++)
            {
                weapon.IncreaseDamage(damageData[i]);
            }
        }
      
    }
}