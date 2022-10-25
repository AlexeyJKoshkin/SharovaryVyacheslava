using System.Linq;
using GameKit;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel
{
    public class SelectBuffScenario : WindowScenario<BuffSelectWindowView>, ISelectBuffScenario
    {
        private ILevelBuff[] _generatedBuffs;

        private readonly ILevelBuffStorage _buffStorage;

        public SelectBuffScenario(BuffSelectWindowView view, ILevelBuffStorage buffStorage)
        {
            _buffStorage = buffStorage;
            InitView(view);
        }

        public void DoShowExpBuffs()
        {
            _generatedBuffs = _buffStorage.GenerateBuffs();
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
            buffBtn.TurnOn();
            buffBtn.Text = generatedBuff.GetType().Name;
            buffBtn.AddCallback(() => OnSelectBufHandler(generatedBuff));
        }

        void OnSelectBufHandler(ILevelBuff selectedBuff)
        {
            selectedBuff.Activate();

            if (!selectedBuff.IsSingle) //если текущий баф можно использовать повторно
                _buffStorage.Return(selectedBuff);

            _generatedBuffs.Where(o => o != selectedBuff) // остальные тоже возвращаем 
                           .ForEach(_buffStorage.Return); // возвращаем

            FinishSuccess();
        }
    }
}
