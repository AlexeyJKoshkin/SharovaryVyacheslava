namespace Core.UserProfile
{
    public class InventoryProfileProgressFacade : UserProgressPartFacade<ProfileInventoryProgress>, IInventoryProgress, IHeroEquipment
    {
        protected override string Key => "Inventory";
        public IHeroEquipment Equipment => this;

        public string EquippedWeaponId => Progress.EquipItemsProgress.EquipWeaponId;
        public InventoryProfileProgressFacade(IUserProgressPartSaveLoader progressAdapter) : base(progressAdapter) { }
    }
}