using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using GameKit;

namespace RoyalAxe.LevelBuff
{
    public class LevelBuffStorage : ILevelBuffStorage
    {
        public const int MAX_REWARDS = 3; //todo: в конфиг перенести
        private readonly HashSet<ILevelBuff> _allExistsRewards = new HashSet<ILevelBuff>();

        public LevelBuffStorage(IReadOnlyList<ILevelBuff> allbuffs)
        {
            allbuffs.ForEach(e=> _allExistsRewards.Add(e));
        }


        public ILevelBuff[] GenerateBuffs()
        {
            var result = new ILevelBuff[MAX_REWARDS];

            var copy = _allExistsRewards.ToList();
            for (int i = 0; i < MAX_REWARDS; i++)
            {
                var reward = copy.GetRandom(true);
                result[i] = reward;
                if (reward.IsSingle)
                {
                    _allExistsRewards.Remove(reward); // чтобы больше не появлялся в этом уровне
                }
            }

            return result;
        }

        public void Return(ILevelBuff buf)
        {
            _allExistsRewards.Add(buf);
        }
        
        
        public void TempDoAll()
        {
         _allExistsRewards.ForEach(e=> e.Activate());
        }
    }
}