using Entitas;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public abstract class BaseViewComponent<TView> : IComponent where TView : Component
    {
        public TView View;
    }
    
    public abstract class RenderComponent<T> : BaseViewComponent<T> where T : Component
    {
        public abstract void SetColor(Color color);
    }
}