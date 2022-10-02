using System;
using GameKit;

namespace RoyalAxe
{
    [Flags]
    public enum RoyalAxeTagNames
    {
        [CustomEnumName("Без тэга")] None = 0,
        [CustomEnumName("Враги")] Enemy = 1 << 1,
        [CustomEnumName("Скилл игрока")] PlayerBoson = 1 << 2,
        [CustomEnumName("Игрок")] Player = 1 << 3, 
        [CustomEnumName("Скилл игрока")] EnemyBoson = 1 << 4,
    }
}