using System.Collections;
using System.Collections.Generic;
using Entitas;

namespace RoyalAxe.GameEntitas
{
    /// <summary>
    ///    Вооще весь урон который наносит игрок
    /// </summary>
    [Units]
    public class DamageComponent : ListCollectionComponent<IInfluenceApplierComposite>
    {
        public IInfluenceApplierComposite MainSimpleInfluence => Count == 0 ? null : Collection[0];
        
      
    }

    [Units]
    public class DeBuffInfluenceComponent : HashSetCollectionComponent<IDeBuffApplier>
    {
        
    }
}
