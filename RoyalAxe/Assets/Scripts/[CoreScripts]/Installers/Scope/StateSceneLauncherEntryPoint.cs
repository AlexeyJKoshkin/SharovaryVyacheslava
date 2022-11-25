using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using UnityEngine;
using VContainer.Unity;

namespace Core.Launcher
{
    class StateSceneLauncherEntryPoint : IInitializable
    {
        private readonly IProjectSceneState _state;
     
        private readonly DataBox[] _sceneData;
        private readonly GameRootLoopContext _context;
        private readonly IRoyalAxeFeatureBuilder _systemsBuilder;
        public StateSceneLauncherEntryPoint(DataBox[] sceneData,
                                            IRoyalAxeFeatureBuilder systemsBuilder,
                                            IProjectSceneState state,
                                            GameRootLoopContext context)
        {
            _sceneData = sceneData;
            _systemsBuilder = systemsBuilder;
            _state = state;
            _context = context;
        }

        public void Initialize()
        {
            var stateEntity = _context.CreateEntity();
            stateEntity.AddAdditionalDataBox(_sceneData);
            
            var update = _systemsBuilder.GetAlwaysUpdateFeature().ToArray(); 
            stateEntity.AddUpdateSystems(update);
            update = _systemsBuilder.GetPauseableUpdateFeature().ToArray();
            stateEntity.AddPauseableUpdateSystems(update);
            stateEntity.AddMainLoopState(_state);
        }
    }
}
