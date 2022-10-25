using System;

namespace RoyalAxe.CoreLevel
{
    /// <summary>
    ///  в текущей реализации UI команда это набор действий при вызове какого-либо окна.
    /// </summary>
    public interface IUICommand
    {
        void ExecuteCommand(Action<bool> onDoneExecuteCommand = null);
        
        public struct UIHandler
        {
            private Action<bool> _callback;

            public UIHandler(Action<bool> callback)
            {
                _callback = callback;
            }

            public void FireCallback(bool isSuccess)
            {
                var temp = _callback;
                _callback = null;
                temp?.Invoke(isSuccess);
            }
        }
    }

    
}
