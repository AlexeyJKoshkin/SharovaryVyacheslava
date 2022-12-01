using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Data.Provider;
using GameKit;

namespace RoyalAxe.LevelBuff
{
    public class LevelBuffStorage : ILevelBuffStorage
    {
        public IEnumerable<LevelBuffType> UnActiveBuffs() => _allExistsRewards.Values.Where(o=>!o.IsActive).Select(o=>o.Type);
        public IReadOnlyCollection<LevelBuffType> ExistsBuffs => _allExistsRewards.Keys;
        
        private readonly Dictionary<LevelBuffType,ILevelPowerStrategy> _allExistsRewards = new Dictionary<LevelBuffType, ILevelPowerStrategy>();

        public LevelBuffStorage(IReadOnlyList<ILevelPowerStrategy> allBuffs)
        {
            allBuffs.ForEach(e=> _allExistsRewards.Add(e.Type,e));
        }

        public ILevelPowerStrategy Get(LevelBuffType type)
        {
            if (_allExistsRewards.TryGetValue(type, out var buff))
            {
                if (buff.IsSingle)
                    _allExistsRewards.Remove(type);
            }
            else
            {
                HLogger.LogError($"Not Found Buff {type}. Check if you bind it at LevelBuffsInstaller.cs");
            }
            return buff;
        }
        
        public ILevelPowerStrategy Peek(LevelBuffType type)
        {
            if (_allExistsRewards.TryGetValue(type, out var buff))
            {
                return buff;
            }
            HLogger.LogError($"Not Found Buff {type}. Check if you bind it at LevelBuffsInstaller.cs");
            return null;
        }
        

        public void ReturnUnUsedSingle(ILevelPowerStrategy buf)
        {
            if(buf.IsSingle)
                _allExistsRewards.Add(buf.Type, buf);
        }
    }
}