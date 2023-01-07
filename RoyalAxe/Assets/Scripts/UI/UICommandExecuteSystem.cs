using System.Collections.Generic;
using Entitas;
using FluentBehaviourTree;

namespace RoyalAxe.UI 
{
    
    public class UICommandExecuteSystem : IExecuteSystem, IInitializeSystem,IUICommandExecuteSystem,IUIScenarioStorage
    {
        private readonly HashSet<IUIBehaviour> ExecutedCommands = new HashSet<IUIBehaviour>();
        private readonly List<IUIBehaviour> TempFinishedIndexes = new List<IUIBehaviour>();
        
        private readonly IReadOnlyList<IUIBehaviour> _allExistBehaviours;
         
        public UICommandExecuteSystem(IReadOnlyList<IUIBehaviour> allExistBehaviours)
        {
            _allExistBehaviours = allExistBehaviours;
        }

        //todo: переделать на лейзи биндинг вьюшки в сценарий
        public T GetScenario<T>() where T :IUIBehaviour
        {
            for (int i = 0; i < _allExistBehaviours.Count; i++)
            {
                if (_allExistBehaviours[i] is T res)
                    return res;
                // тут еще получать вьюшку
            }
            return default;
        }
        

        public void Execute()
        {
            TempFinishedIndexes.Clear();
            TickBehaviour(TimeData.Last);
            ClearFinished();
            
        }

        private void ClearFinished()
        {
            for (int i = 0; i < TempFinishedIndexes.Count; i++)
            {
                var b = TempFinishedIndexes[i];
                ExecutedCommands.Remove(b);
                b.ExitState();
            }
        }

        private void TickBehaviour(TimeData last)
        {
            foreach (var c in ExecutedCommands)
            {
                if (c.Execute(last) != BehaviourTreeStatus.Running)
                {
                    TempFinishedIndexes.Add(c);
                }
            }
        }

        public void Initialize()
        {
        }

        public void Execute(IUIBehaviour behaviour)
        {
           
            if(ExecutedCommands.Add(behaviour))
                behaviour.EnterState();
        }
    }
}