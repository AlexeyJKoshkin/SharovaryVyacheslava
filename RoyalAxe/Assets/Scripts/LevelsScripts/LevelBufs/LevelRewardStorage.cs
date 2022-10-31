using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using GameKit;

namespace RoyalAxe.LevelBuff
{
    public class LevelBuffStorage : ILevelBuffStorage
    {
        public IReadOnlyCollection<LevelBuffType> ExistsBuffs => _allExistsRewards.Keys;
        
        
        private readonly Dictionary<LevelBuffType,ILevelBuff> _allExistsRewards = new Dictionary<LevelBuffType, ILevelBuff>();

        public LevelBuffStorage(IReadOnlyList<ILevelBuff> allBuffs)
        {
            allBuffs.ForEach(e=> _allExistsRewards.Add(e.Type,e));
        }

        public ILevelBuff Get(LevelBuffType type)
        {
            if (_allExistsRewards.TryGetValue(type, out var buff))
            {
                if (buff.IsSingle)
                    _allExistsRewards.Remove(type);
            }
            return buff;
        }
        
      
        

        public void ReturnUnUsedSingle(ILevelBuff buf)
        {
            if(buf.IsSingle)
                _allExistsRewards.Add(buf.Type, buf);
        }
    }
}