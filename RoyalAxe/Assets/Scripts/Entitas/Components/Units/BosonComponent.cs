using Entitas;

namespace RoyalAxe.GameEntitas
{
    [Units]
    public class BosonComponent : IComponent
    {
    }
    
    /// <summary>
    ///     Указатель что это переносчик взаимодействия от игрока к мобам
    /// </summary>
    [Units]
    public class PlayerBosonComponent : IComponent
    {
        public bool IsComeBack => CountInteraction > 1;
        public int CountInteraction;
    }
}
