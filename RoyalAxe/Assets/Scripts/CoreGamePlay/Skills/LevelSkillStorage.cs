using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using GameKit;

namespace RoyalAxe.LevelSkill 
{
    static class ILevelSkillStorageExtension
    {
        public static IEnumerable<LevelSkillType> UnActiveBuffs(this ILevelSkillStorage storage)
        {
            return storage.Where(o=>!o.IsActive).Select(o=>o.Type);  
        } 
    }

    
    public class LevelSkillStorage : ILevelSkillStorage
    {
        
        private readonly Dictionary<LevelSkillType,ILevelSkill> _allExistsRewards = new Dictionary<LevelSkillType, ILevelSkill>();

        public LevelSkillStorage(IReadOnlyList<ILevelSkill> allSkills)
        {
            allSkills.ForEach(e=> _allExistsRewards.Add(e.Type,e));
        }

        public ILevelSkill Get(LevelSkillType type)
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
        
        public ILevelSkill Peek(LevelSkillType type)
        {
            if (_allExistsRewards.TryGetValue(type, out var buff))
            {
                return buff;
            }
            HLogger.LogError($"Not Found Buff {type}. Check if you bind it at LevelBuffsInstaller.cs");
            return null;
        }

        public IEnumerator<ILevelSkill> GetEnumerator()
        {
            return _allExistsRewards.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _allExistsRewards.Values.Count;
    }
}