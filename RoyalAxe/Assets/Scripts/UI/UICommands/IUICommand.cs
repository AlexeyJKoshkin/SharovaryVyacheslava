using System;
using Core;

namespace RoyalAxe.UI
{
    public interface IUIBehaviour : IFMSState
    {
    }

    public interface IUICommandExecuteSystem
    {
        void Execute(IUIBehaviour winLevelUiCommand);
    }
}
