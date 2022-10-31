using System.Collections.Generic;
using System.Linq;
using GameKit;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.LevelBuff
{
    public interface ILevelBuffStorage
    {
        IReadOnlyCollection<LevelBuffType> ExistsBuffs { get; }
        void ReturnUnUsedSingle(ILevelBuff buff);
        ILevelBuff Get(LevelBuffType reward);
    }

    public interface ICurrentLevelBuffDistributor
    {
        ILevelBuff[] GenerateBuff();
        void HandleSelection(ILevelBuff selectedBuff);
    }

    public class CurrentLevelBuffDistributor : ICurrentLevelBuffDistributor
    {
        public const int MAX_REWARDS = 3;
        private readonly ILevelBuffStorage _storage;
        private ILevelWaveProvider _levelWaveProvider;
        private ILevelBuff[] _cashed;


        public CurrentLevelBuffDistributor(ILevelBuffStorage storage, ILevelWaveProvider levelWaveProvider)
        {
            _storage           = storage;
            _levelWaveProvider = levelWaveProvider;
        }


        public ILevelBuff[] GenerateBuff()
        {
            LevelBuffType[] buffs = _levelWaveProvider.CurrentBuffs;

            _cashed = (buffs == null || buffs.Length == 0)
                ? GenerateRandom()
                : buffs.Select(_storage.Get).ToArray();
            return _cashed;
        }

        public void HandleSelection(ILevelBuff selectedBuff)
        {
            foreach (var buff in _cashed)
            {
                if (CheckCanReturn(buff))
                    _storage.ReturnUnUsedSingle(buff); // вернем только неиспользуемый сингловый баф
            }

            bool CheckCanReturn(ILevelBuff buff) // возращаем только не текущий баф и одноразовый
            {
                return buff != selectedBuff && buff.IsSingle;
            }

            _cashed = null;
        }

        private ILevelBuff[] GenerateRandom()
        {
            var result = new ILevelBuff[MAX_REWARDS];
            var copy   = _storage.ExistsBuffs.ToList();
            for (int i = 0; i < MAX_REWARDS; i++)
            {
                var reward = copy.GetRandom(true);
                result[i] = _storage.Get(reward);
            }

            return result;
        }
    }
}