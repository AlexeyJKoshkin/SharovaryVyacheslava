using System.Collections.Generic;
using Entitas;
using GameKit;
using RoyalAxe.EntitasSystems;

namespace Core.Launcher
{
    public class SceneSystemBuilder<T> : IRoyalAxeCoreSystemsBuilder where T : ISystem
    {
        private readonly List<T> _alwaysUpdateSystems = new List<T>();

        private readonly List<T> _pauseAbleSystems = new List<T>();

        public SceneSystemBuilder(IReadOnlyList<T> royalAxeSystems)
        {
            royalAxeSystems.ForEach(s =>
                                    {
                                        if (s is IPauseAbleSystem)
                                        {
                                            _pauseAbleSystems.Add(s);
                                        }
                                        else
                                        {
                                            _alwaysUpdateSystems.Add(s);
                                        }
                                    });
        }

        public Feature BuildUpdate(string featureName)
        {
            return BuildUpdateSystem(featureName, _alwaysUpdateSystems);
        }

        public Feature BuildPauseableUpdate(string featureName)
        {
            return BuildUpdateSystem(featureName, _pauseAbleSystems);
        }

        private Feature BuildUpdateSystem(string featureName, List<T> systems)
        {
            if (systems.Count == 0)
            {
                return null;
            }

            var result = new Feature(featureName);
            systems.ForEach(e => result.Add(e));
            return result;
        }
    }
}