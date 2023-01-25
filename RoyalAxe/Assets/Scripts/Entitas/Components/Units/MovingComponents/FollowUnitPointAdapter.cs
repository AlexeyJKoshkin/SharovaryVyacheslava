using System;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    //Движение к юниту
    [Serializable]
    public class FollowUnitPointAdapter : FollowTargetPointAdapter
    {
        private readonly UnitsEntity _unitEntity;
        public UnitsEntity Unit => _unitEntity;

        public FollowUnitPointAdapter(UnitsEntity unitEntity):base(unitEntity.unitsView.RootTransform)
        {
            _unitEntity = unitEntity;
        }
    }
}
