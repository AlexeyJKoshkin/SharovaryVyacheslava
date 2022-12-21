using Entitas;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.GameEntitas 
{
    /// <summary>
    /// основной урон персонажа
    /// </summary>
    [Units]
    public class MainDamageComponent : IComponent
    {
        public IUnitMainItem Influence;
     
    
        

        public float GetSingleValue(DamageType physical)
        {
            return Influence.GetSingleValue(physical);
        }
    }
}