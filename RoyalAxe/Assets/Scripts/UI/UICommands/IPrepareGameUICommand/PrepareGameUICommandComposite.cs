using System.Collections.Generic;

namespace RoyalAxe.UI
{
    public interface IPrepareGameUICommandComposite : IPrepareGameUICommand
    {
        
    }
    
    public class PrepareGameUICommandComposite : IPrepareGameUICommandComposite
    {
        private readonly IReadOnlyList<IPrepareGameUICommand> _allCommands;
        public PrepareGameUICommandComposite(IReadOnlyList<IPrepareGameUICommand> allCommands)
        {
            _allCommands = allCommands;
        }

        public void PrepareUIStartGame()
        {
            for (int i = 0; i < _allCommands.Count; i++)
            {
                _allCommands[i].PrepareUIStartGame();
            }
        }
    }
}