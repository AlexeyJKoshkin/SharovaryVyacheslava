using FluentBehaviourTree;
using RoyalAxe;
using RoyalAxe.Configs;
using RoyalAxe.CoreLevel;
using RoyalAxe.GameEntitas;
using RoyalAxe.LevelBuff;
using RoyalAxe.Map;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Core.Launcher
{
    [CreateAssetMenu(menuName = RoyalAxeCreateAssetMenu.GameStateMenuPath + "TestCreateMobState", fileName = "TestCreateMobState")]
    public class TestCreateMobState : AbstractSceneStateScriptable
    {
        [Inject]
        private ILevelCreation _levelCreation;
        [Inject]
        private readonly ILevelDirector _levelDirector;
        
        [Inject] private IUnitsBuilderFacade _builderFacade;
        
        [Inject] private ILevelBuffStorage _levelBuffStorage;

        [SerializeField] private UnitConfigDef _testMob;

        [SerializeField] private UnitConfigDef _testRange;
        
        

        protected override void OnExecute(TimeData dt) { }

        protected override void OnEnterState()
        {
            /*_builderFacade.CreateEnemyMobUnit(_testMob.UniqueID, 1, _testMob.Prefab.RootTransform.position);
            _builderFacade.CreateEnemyMobUnit(_testRange.UniqueID, 1,_testRange.Prefab.RootTransform.position);*/
            
           var indfrastructure = _levelCreation.CreateLevel();
            //по хорошему надо ждать анимации, всякое такое
            _levelDirector.StartLevel(indfrastructure);
            //стейт начал работать

            //  _axeSceneDependenciesConnector.EnterState(this);   
        }

        protected override void OnExitState()
        {
            //  _axeSceneDependenciesConnector.ExitState(this);
        }

        [Button]
        void ActivateAll()
        {
            _levelBuffStorage.TempDoAll();
        }
    }
}