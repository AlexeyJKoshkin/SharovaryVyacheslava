using System;
using Core;
using FluentBehaviourTree;

namespace RoyalAxe.UI 
{
   
    /// <summary>
    /// Сценарий - это "контроллер" окна. Описываем сценарий повдения КОНКРЕТНОГО ОКНА.
    /// по задумке все сценарии создаеются заранее. а вьюшку им подпихиваем по необходимости.
    /// сейчас реализовано так, что вьюшка запихивается в конструкторе
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UIScenario<T> :IUIBehaviour where T : UIViewComponent
    {
        public T View { get; private set; }

        private BehaviourTreeStatus _status = BehaviourTreeStatus.Running;
        public string NodeName => this.GetType().Name;

        public virtual void InitView(T view)
        {
            if (View != null && view != null)
            {
                HLogger.LogError("View is already exists");
                return;
            }
            View = view;
        }

        public BehaviourTreeStatus Execute(TimeData time)
        {
            return _status;
        }

        protected void Continue()
        {
            _status = BehaviourTreeStatus.Running;
        }

        protected void FinishSuccess()
        {
            _status = BehaviourTreeStatus.Success;
        }

        protected void FinishFail()
        {
            _status = BehaviourTreeStatus.Failure;
        }

        public virtual void ExitState() { }

        public virtual void EnterState() { }
    }

    public class CoreGamePlayMainUIScenario : UIScenario<CoreGameSceneUIView>
    {
        
    }
}