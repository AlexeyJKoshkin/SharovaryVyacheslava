using System;
using System.Collections.Generic;
using Core.Data.Provider;
using UnityEngine;

namespace Core.Launcher
{
    [Serializable]
    public abstract class ProjectStateSettings : IRoyalAxeSceneFeatureProvider
    {
        public abstract IEnumerable<Feature> EventListenerSystem(Contexts contexts);

        public abstract IEnumerable<FeatureBindInfo> AlwaysUpdate();
        public abstract IEnumerable<FeatureBindInfo> PauseableUpdate();
        public abstract IEnumerable<Type> AllSystems();

        public abstract void CreateFeatureBlanks();
    }
}