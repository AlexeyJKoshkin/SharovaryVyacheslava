using System.Collections.Generic;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.CoreGamePlay
{
    public interface IModificatorProvider
    {
        IEnumerable<ICharacterStatModificator> ApplyTempStats(UnitsEntity target);
        void ApplyPermanentStatMods(UnitsEntity target);
    }
}