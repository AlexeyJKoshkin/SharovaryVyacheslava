using System;
using System.Collections.Generic;
using RoyalAxe;

namespace Core
{
    public static class ProjectEnumExtension
    {
        public static RoyalAxeTagNames SetFlag(this RoyalAxeTagNames a, RoyalAxeTagNames b)
        {
            return a | b;
        }

        public static RoyalAxeTagNames UnsetFlag(this RoyalAxeTagNames a, RoyalAxeTagNames b)
        {
            return a & ~b;
        }

        public static RoyalAxeTagNames ToggleFlag(this RoyalAxeTagNames a, RoyalAxeTagNames b)
        {
            return a ^ b;
        }

        public static bool HasFlag(this RoyalAxeTagNames a, RoyalAxeTagNames b)
        {
            return (a & b) == b;
        }

        public static IEnumerable<RoyalAxeTagNames> All(this RoyalAxeTagNames a)
        {
            foreach (RoyalAxeTagNames d in Enum.GetValues(typeof(RoyalAxeTagNames)))
                if (a.HasFlag(d))
                {
                    yield return d;
                }
        }
    }
}