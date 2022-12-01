using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.GameEntitas {
    [GameRootLoop, Unique]
    public class CheatsComponent : IComponent
    {
        public UltimateCheatSettings CheatSettings;
    }
    
}