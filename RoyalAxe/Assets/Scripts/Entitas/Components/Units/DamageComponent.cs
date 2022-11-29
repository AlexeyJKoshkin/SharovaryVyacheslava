
namespace RoyalAxe.GameEntitas
{
    /// <summary>
    ///    Вооще весь урон который наносит игрок
    /// </summary>
    [Units]
    public class DamageComponent : ListCollectionComponent<IInfluenceApplier>
    {
        public IInfluenceApplierComposite MainInfluence;
    }
}
