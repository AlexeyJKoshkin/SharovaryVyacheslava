using System;
using System.Linq;
using Core;
using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public delegate void EndWindowScenario(bool isSuccess);
    
   

    /// <summary>
    /// Сценарий - это "контроллер" окна. Описываем сценарий повдения КОНКРЕТНОГО ОКНА.
    /// по задумке все сценарии создаеются заранее. а вьюшку им подпихиваем по необходимости.
    /// сейчас реализовано так, что вьюшка запихивается в конструкторе
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class WindowScenario<T> :IWindowScenario, IBehaviourTreeNode where T : UIViewComponent
    {
        public T View { get; private set; }

        public event EndWindowScenario OnFinishScenarioEvent
        {
            add
            {
                _onFinishScenario -= value;
                _onFinishScenario += value;
                
            }
            remove { _onFinishScenario -= value; }
        }
        private BehaviourTreeStatus _status = BehaviourTreeStatus.Running;
        private EndWindowScenario _onFinishScenario;
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

        private void FireEndCallBack()
        {
            var temp = _onFinishScenario;
            _onFinishScenario = null;
            temp?.Invoke(_status == BehaviourTreeStatus.Success);
        }

        protected void FinishSuccess()
        {
            _status = BehaviourTreeStatus.Success;
            FireEndCallBack();
        }

        protected void FinishFail()
        {
            _status = BehaviourTreeStatus.Failure;
            FireEndCallBack();
        }
    }
}
