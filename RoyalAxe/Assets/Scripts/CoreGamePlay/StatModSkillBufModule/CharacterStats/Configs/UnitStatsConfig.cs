using System.Collections.Generic;

namespace RoyalAxe.Configs
{
    public interface IStatableEntityConfig
    {
        IEnumerable<EntityStatConfig> All();
    }
}