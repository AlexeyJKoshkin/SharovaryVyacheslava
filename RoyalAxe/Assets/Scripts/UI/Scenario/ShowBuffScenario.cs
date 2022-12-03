using System.Linq;
using GameKit;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel
{
    public class SelectBuffScenario : WindowScenario<BuffSelectWindowView>, ISelectBuffScenario
    {
        private ILevelSkill[] _generatedBuffs;

  
        private readonly ICurrentLevelSkillDistributor _currentWaveDistributor;

        public SelectBuffScenario(BuffSelectWindowView view, ICurrentLevelSkillDistributor currentWaveDistributor)
        {
            _currentWaveDistributor = currentWaveDistributor;
            InitView(view);
        }

        public void DoShowExpBuffs()
        {
            _generatedBuffs = _currentWaveDistributor.GenerateSkill();
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

        private void InitBuff(BuffSelectWindowView.BuffBntView buffBtn, ILevelSkill generatedPowerStrategy)
        {
            if (generatedPowerStrategy == null)
            {
                buffBtn.Reset();
                return;
            }

            buffBtn.TurnOn();
            buffBtn.Text = generatedPowerStrategy.GetType().Name;
            buffBtn.AddCallback(() => OnSelectBufHandler(generatedPowerStrategy));
        }

        void OnSelectBufHandler(ILevelSkill selectedPowerStrategy)
        {
            selectedPowerStrategy.Activate();
            View.Close();
            FinishSuccess();
        }
    }
}