using System.Collections.Generic;
using System.Linq;
using GameKit;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.LevelBuff
{
    public interface ILevelBuffStorage
    {
        IEnumerable<LevelBuffType> UnActiveBuffs();
       IReadOnlyCollection<LevelBuffType> ExistsBuffs { get; }
        void ReturnUnUsedSingle(ILevelPowerStrategy powerStrategy);
        ILevelPowerStrategy Get(LevelBuffType reward);
        ILevelPowerStrategy Peek(LevelBuffType type);
    }

    public interface ICurrentLevelBuffDistributor
    {
        ILevelPowerStrategy[] GenerateBuff();
        void HandleSelection(ILevelPowerStrategy selectedPowerStrategy);
    }

    public class CurrentLevelBuffDistributor : ICurrentLevelBuffDistributor
    {
        public const int MAX_REWARDS = 3;
        private readonly ILevelBuffStorage _storage;
        private CoreGamePlayContext _coreGamePlay;
        private ILevelPowerStrategy[] _cashed;


        public CurrentLevelBuffDistributor(ILevelBuffStorage storage, CoreGamePlayContext levelWaveProvider)
        {
            _storage           = storage;
            _coreGamePlay = levelWaveProvider;
        }


        public ILevelPowerStrategy[] GenerateBuff()
        {
            LevelBuffType[] buffs = _coreGamePlay.levelWaveEntity.hasWizardShopReady ? _coreGamePlay.levelWaveEntity.wizardShopReady.LevelBuffTypes : null;

            _cashed = (buffs == null || buffs.Length == 0)
                ? GenerateRandom()
                : buffs.Select(_storage.Get).ToArray();
            return _cashed;
        }

        public void HandleSelection(ILevelPowerStrategy selectedPowerStrategy)
        {
            foreach (var buff in _cashed)
            {
                if (CheckCanReturn(buff))
                    _storage.ReturnUnUsedSingle(buff); // вернем только неиспользуемый сингловый баф
            }

            bool CheckCanReturn(ILevelPowerStrategy buff) // возращаем только не текущий баф и одноразовый
            {
                return buff != null && buff != selectedPowerStrategy && buff.IsSingle;
            }

            _cashed = null;
        }

        private ILevelPowerStrategy[] GenerateRandom()
        {
            var result = new ILevelPowerStrategy[MAX_REWARDS];
            var copy   = _storage.UnActiveBuffs().ToList();
            for (int i = 0; i < MAX_REWARDS; i++)
            {
                var reward = copy.GetRandom(true);
                result[i] = _storage.Get(reward);
            }

            return result;
        }
    }
}