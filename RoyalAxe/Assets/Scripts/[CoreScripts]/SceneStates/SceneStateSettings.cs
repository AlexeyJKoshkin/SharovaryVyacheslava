using System;
using System.Collections.Generic;

namespace Core.Launcher
{
    [Serializable]
    public class DefaultProjectStateSettings : ProjectStateSettings
    {
        public override IEnumerable<Feature> EventListenerSystem(Contexts contexts)
        {
            yield break;
        }

        public override IEnumerable<FeatureBindInfo> AlwaysUpdate()
        {
            yield break;
        }

        public override IEnumerable<FeatureBindInfo> PauseableUpdate()
        {
            yield break;
        }

        public override IEnumerable<Type> AllSystems()
        {
            yield break;
        }

        public override void CreateFeatureBlanks() { }
    }
}