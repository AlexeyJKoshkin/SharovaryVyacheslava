using System.Collections;
using System.Collections.Generic;
using Entitas;

namespace RoyalAxe.GameEntitas
{
    /// <summary>
    ///    Вооще весь урон который наносит игрок
    /// </summary>
    [Units]
    public class DamageComponent : IComponent, IEnumerable<IInfluenceApplier>
    {
        public ISimpleInfluenceApplier MainSimpleInfluence => SingleDamage.Count == 0 ? null : SingleDamage[0];
        
        public List<ISimpleInfluenceApplier> SingleDamage;
        public List<IPeriodicInfluenceApplier> PeriodicDamage;
        public IEnumerator<IInfluenceApplier> GetEnumerator()
        {
            for (int i = 0; i < SingleDamage.Count; i++)
            {
                yield return SingleDamage[i];
            }
            
            for (int i = 0; i < PeriodicDamage.Count; i++)
            {
                yield return PeriodicDamage[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [Units]
    public class DeBuffInfluenceComponent : HashSetCollectionComponent<IDeBuffApplier>
    {
        
    }
}
