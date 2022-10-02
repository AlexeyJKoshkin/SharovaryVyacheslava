using RoyalAxe.Configs;
using RoyalAxe.Units;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public interface IUnitsViewBuilder
    {
        UnitsView BuildMobView(UnitsEntity unitsEntity, Vector2 pos);
        UnitsView BuildPlayerView(UnitsEntity player);
        BosonView BuildBosonView(UnitsEntity boson, UnitConfigDef bosonViewConfig, Vector3 pos);
    }
}