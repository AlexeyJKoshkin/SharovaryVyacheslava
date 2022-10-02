using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using GameKit;
using UnityEngine;
using VContainer;

namespace Core.Launcher
{
    public interface IRoyalAxeFeatureBuilder
    {
        IEnumerable<Feature> GetPauseableUpdateFeature();
        IEnumerable<Feature> GetAlwaysUpdateFeature();
    }

    public class FeatureBindInfo
    {
        public string FeatureName;
        public IEnumerable<Type> FeatureSystems => _types;
        private HashSet<Type> _types = new HashSet<Type>();

        public static FeatureBindInfo Create(string name, params Type[] types)
        {
            var result = new FeatureBindInfo {FeatureName = name,};
            types?.ForEach(t => result._types.Add(t));
            return result;
        }

        public FeatureBindInfo Bind<T>()
        {
            _types.Add(typeof(T));
            return this;
        }
    }

    public interface IRoyalAxeSceneFeatureProvider
    {
        IEnumerable<Feature> EventListenerSystem(Contexts contexts);
        
        IEnumerable<FeatureBindInfo> AlwaysUpdate();
        IEnumerable<FeatureBindInfo> PauseableUpdate();
    }

    public class RoyalAxeFeatureBuilder : IRoyalAxeFeatureBuilder
    {
        private readonly IRoyalAxeSceneFeatureProvider _provider;
        private readonly IObjectResolver _container;
        private readonly Contexts _contexts;
        public RoyalAxeFeatureBuilder(IRoyalAxeSceneFeatureProvider provider, IObjectResolver container, Contexts contexts)
        {
            _provider  = provider;
            _container = container;
            _contexts = contexts;
        }

        public IEnumerable<Feature> GetPauseableUpdateFeature()
        {
            return Build(_provider.PauseableUpdate());
        }

        public IEnumerable<Feature> GetAlwaysUpdateFeature()
        {
            foreach (var blank in _provider.AlwaysUpdate())
            {
                yield return BuildFeature(blank);
            }

            foreach (var listener in _provider.EventListenerSystem(_contexts))
            {
                yield return listener;
            }
        }

        private IEnumerable<Feature> Build(IEnumerable<FeatureBindInfo> data)
        {
            foreach (var featureBindInfo in data) yield return BuildFeature(featureBindInfo);
        }

        private Feature BuildFeature(FeatureBindInfo featureBindInfo)
        {
            var result = new Feature(featureBindInfo.FeatureName);
            featureBindInfo.FeatureSystems
                           .Select(e => _container.Resolve(e) as ISystem)
                           .ForEach(s => { result.Add(s); });

            return result;
        }
    }
}