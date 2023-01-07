
namespace RoyalAxe.UI 
{
    public interface IUIScenarioExecutor
    {
        void ExecuteWinUIScenario();
        void ExecuteSelectBufUIScenario();
        void ExecuteLoseUIScenario();
    }

    public interface IUIScenarioStorage
    {
        T GetScenario<T>() where T : IUIBehaviour;
    }


    public class UIScenarioExecutor : IUIScenarioExecutor
    {
        //потом надо переделать на лейзи биндиг
        private readonly IUICommandExecuteSystem _uiCommandExecuteSystem;
        private readonly IUIScenarioStorage _scenarioStorage;
        
        public UIScenarioExecutor(IUICommandExecuteSystem uiCommandExecuteSystem, IUIScenarioStorage scenarioStorage)
        {
            _uiCommandExecuteSystem = uiCommandExecuteSystem;
            _scenarioStorage = scenarioStorage;
        }

        public void ExecuteWinUIScenario()
        {
            Execute<WinWindowShowScenario>();
 
        }

        public void ExecuteSelectBufUIScenario()
        {
            Execute<ShowSelectBuffWindowCommand>();
        }

        public void ExecuteLoseUIScenario()
        {
            Execute<LoseLevelUIScenario>();
        }

        void Execute<T>() where T: IUIBehaviour
        {
            var scenario = _scenarioStorage.GetScenario<T>();
            _uiCommandExecuteSystem.Execute(scenario);
        }
    }
}