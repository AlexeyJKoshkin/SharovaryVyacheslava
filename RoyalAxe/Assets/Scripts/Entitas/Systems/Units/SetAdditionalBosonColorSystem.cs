using Entitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class SetAdditionalBosonColorSystem : IInitializeSystem, ITearDownSystem
    {
        private readonly IGroup<UnitsEntity> _additionalBosons;

        public SetAdditionalBosonColorSystem(UnitsContext unitsContext)
        {
            _additionalBosons = unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.SpriteRender, UnitsMatcher.AdditionalBoson));
        }

        public void Initialize()
        {
            _additionalBosons.OnEntityAdded += AdditionalBosonsOnOnEntityAdded;
        }

        public void TearDown()
        {
            _additionalBosons.OnEntityAdded -= AdditionalBosonsOnOnEntityAdded;
        }

        private void AdditionalBosonsOnOnEntityAdded(IGroup<UnitsEntity> @group, UnitsEntity entity, int index, IComponent component)
        {
            entity.spriteRender.SetColor(new Color(1, 1, 1, 0.6f));
        }
    }
}
