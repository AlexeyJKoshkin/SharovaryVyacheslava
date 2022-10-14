using System;
using System.Linq;
using Core.Installers;
using GameKit;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel 
{
    public interface IShowBuffCommand
    {
        void DoShowExpBuffs();
    }

    public class ShowBuffCommand : IShowBuffCommand
    {
        private readonly BuffSelectWindowView _buffSelectWindowView;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSystemSwitcher;
        private readonly ILevelBuffStorage _buffStorage;

        public ShowBuffCommand(BuffSelectWindowView buffSelectWindowView, IRoyalAxePauseSystemSwitcher pauseSystemSwitcher, ILevelBuffStorage buffStorage)
        {
            _buffSelectWindowView = buffSelectWindowView;
            _pauseSystemSwitcher = pauseSystemSwitcher;
            _buffStorage = buffStorage;
        }

        public void DoShowExpBuffs()
        {
            _pauseSystemSwitcher.SetPause();
            LevelBuffHandler handler = new LevelBuffHandler(_buffStorage,_buffSelectWindowView,ContinueGame);
            handler.ShowWindow();
        }

        void ContinueGame()
        {
            _pauseSystemSwitcher.UnPause();
            _buffSelectWindowView.Hide();
        }
    }

    class LevelBuffHandler
    {
        private readonly ILevelBuffStorage _levelBuffStorage;
        private readonly BuffSelectWindowView _buffSelectWindowView;
        private readonly Action _continueGame;
        private readonly ILevelBuff[] _generatedBuffs;

        public LevelBuffHandler(ILevelBuffStorage levelBuffStorage,BuffSelectWindowView buffSelectWindowView, Action continueGame)
        {
            _levelBuffStorage = levelBuffStorage;
            _buffSelectWindowView = buffSelectWindowView;
            _continueGame = continueGame;
            _generatedBuffs = _levelBuffStorage.GenerateBuffs();
        }
        
        public void ShowWindow()
        {
            InitBuffs();
            
            _buffSelectWindowView.Show();
            
        }

        private void InitBuffs()
        {
            int index = 0;
            foreach (var buffBtn in _buffSelectWindowView.BuffBtns)
            {
                if (index >= _generatedBuffs.Length) return;
                /*{
                    buffBtn.Reset();
                    continue;
                }*/
                InitBuff(buffBtn, _generatedBuffs[index]);
                index++;
            }
        }

        private void InitBuff(BuffSelectWindowView.BuffBntView buffBtn, ILevelBuff generatedBuff)
        {
            buffBtn.TurnOn();
            buffBtn.Text = generatedBuff.GetType().Name;
            buffBtn.AddCallback(() => OnSelectBufHandler(generatedBuff));
        }

        void OnSelectBufHandler(ILevelBuff selectedBuff)
        {
            selectedBuff.Activate();
            _generatedBuffs.
                Where(o=> o!= selectedBuff || !o.IsSingle) // все бафы, которые не были выбраны
                .ForEach(_levelBuffStorage.Return);        // возвращаем
            
            _continueGame?.Invoke();
            
        }
     
    }
}