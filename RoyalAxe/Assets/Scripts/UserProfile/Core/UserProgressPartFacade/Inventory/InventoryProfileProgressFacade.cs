namespace Core.UserProfile
{
    public class InventoryProfileProgressSaveLoaderAdapter : UserProfileProgressSaveLoaderAdapter<ProfileInventoryProgress>, IInventoryProgress,IHeroEquipment
    {
        protected override string Key => "Inventory";
        public IHeroEquipment Equipment => this;
        
        public string EquippedWeaponId => Progress.EquipItemsProgress.EquipWeaponId;
        
        public InventoryProfileProgressSaveLoaderAdapter(IUserProgressPartFactory<ProfileInventoryProgress> loader) : base(loader) { }

      

        protected override void SetToMainFacade(GeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            currentGeneralUserProgressProfileFacade.InventoryProgress = this;
        }
        
    }
}
