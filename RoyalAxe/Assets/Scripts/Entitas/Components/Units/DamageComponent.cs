using System.Collections;
using System.Collections.Generic;
using Entitas;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    /// <summary>
    ///     Дефолтное оружие ближнего боя
    /// </summary>
    [Units]
    public class DamageComponent : IComponent, IEnumerable<IDamageApplier>
    {
        public List<OneMomentDamageOperation> SingleDamage;
        public List<ElementalDamageBuf.ElementalBufApplyHelper> PeriodicDamage;
        public IEnumerator<IDamageApplier> GetEnumerator()
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
}
