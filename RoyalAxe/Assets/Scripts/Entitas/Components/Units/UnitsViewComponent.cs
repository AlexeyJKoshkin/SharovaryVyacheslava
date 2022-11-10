using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.Units;
using RoyalAxe.Units.UnitBehaviour;
using UnityEngine;

namespace RoyalAxe.GameEntitas {
    /// <summary>
    ///     Компонент вьюшки
    /// </summary>
    [Units, Event(EventTarget.Self)]
    public class UnitsViewComponent : BaseViewComponent<BaseUnitView>
    {
        public Transform RootTransform => View.RootTransform;

        public TView Get<TView>() where TView : BaseUnitView
        {
            return View as TView;
        }
    }
    
    [Units]
    public class SpriteRenderComponent : RenderComponent<SpriteRenderer>
    {
        public override void SetColor(Color color)
        {
            View.color = color;
        }
    }
    [Units]
    public class UnitPhysicColliderComponent : IComponent
    {
        public Collider2D PhysicCollider;
    }
    /// <summary>
    ///     ентити анимации.
    /// </summary>
    [Units]
    public class UnitAnimationEntity : IComponent
    {
        public RAAnimationEntity AnimationEntity;
    }

    [Units]
    public class UnitBehaviorComponent : IComponent
    {
        public IUnitBehaviourNode Behaviour;
    }


}