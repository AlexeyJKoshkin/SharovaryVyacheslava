using System.Linq;
using GameKit;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel
{
    public class SelectBuffScenario : WindowScenario<BuffSelectWindowView>, ISelectBuffScenario
    {
        private ILevelBuff[] _generatedBuffs;

  
        private readonly ICurrentLevelBuffDistributor _currentWaveDistributor;

        public SelectBuffScenario(BuffSelectWindowView view, ICurrentLevelBuffDistributor currentWaveDistributor)
        {
            _currentWaveDistributor = currentWaveDistributor;
            InitView(view);
        }

        public void DoShowExpBuffs()
        {
            _generatedBuffs = _currentWaveDistributor.GenerateBuff();
            InitBuffs();
            View.Open();
            Continue();
        }


        private void InitBuffs()
        {
            int index = 0;

            foreach (var buffBtn in View.BuffBtns)
            {
                if (index >= _generatedBuffs.Length)
                {
                    buffBtn.Reset();
                    continue;
                }

                InitBuff(buffBtn, _generatedBuffs[index]);
                index++;
            }
        }

        private void InitBuff(BuffSelectWindowView.BuffBntView buffBtn, ILevelBuff generatedBuff)
        {
            if (generatedBuff == null)
            {
                buffBtn.Reset();
                return;
            }

            buffBtn.TurnOn();
            buffBtn.Text = generatedBuff.GetType().Name;
            buffBtn.AddCallback(() => OnSelectBufHandler(generatedBuff));
        }

        void OnSelectBufHandler(ILevelBuff selectedBuff)
        {
            selectedBuff.Activate();
            _currentWaveDistributor.HandleSelection(selectedBuff);
            View.Close();
            FinishSuccess();
        }
    }
}