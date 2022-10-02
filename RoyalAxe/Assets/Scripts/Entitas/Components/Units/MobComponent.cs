using System;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.Units;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    /// <summary>
    ///     Указательно что это моб (можно добавить какой-то енам. мол моб такой-то)
    /// </summary>
    [Units]
    public class MobComponent : IComponent { }



    [Units]
    public class MobDeathRewardComponent : IComponent
    {
        public int ExpReward;
        public int GoldReward;
        public int GemReward;
    }
}