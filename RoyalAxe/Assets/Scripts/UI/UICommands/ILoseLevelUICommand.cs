using System;

namespace RoyalAxe.CoreLevel 
{
    public interface ILoseLevelUICommand : IUICommand
    {
    }

    public class LoseLevelUICommand : ILoseLevelUICommand
    {
        private IUICommand.UIHandler _handler;
        public void ExecuteCommand(Action<bool> onDoneExecuteCommand = null)
        {
            _handler = new IUICommand.UIHandler(onDoneExecuteCommand);
        }
    }
}