using Core.Data.Provider;
using RoyalAxe.Units;
using UnityEngine;

namespace RoyalAxe.Configs
{
    public abstract class BaseUnitConfig : ScriptableObject, IDataObject
    {
        public string UniqueID => name;
        [SerializeField] public BaseUnitView Prefab;
    }
}