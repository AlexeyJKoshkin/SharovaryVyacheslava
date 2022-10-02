using Entitas;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    /// <summary>
    ///     Компонент урона (вешается при нанесении урона). в конце кадра снимается
    /// </summary>
    [Units]
    public class HitUnitComponent : IComponent
    {
        public float HitValue => HitDamageInfo.HitValue;
        public DamageType DamageType => HitDamageInfo.DamageType;
        public bool IsCritical => HitDamageInfo.IsCritical;

        public HitDamageInfo HitDamageInfo;
    }
}
