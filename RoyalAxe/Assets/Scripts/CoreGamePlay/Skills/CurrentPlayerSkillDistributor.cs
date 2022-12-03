using System.Collections.Generic;
using System.Linq;
using GameKit;

namespace RoyalAxe.LevelBuff {
    public class CurrentPlayerSkillDistributor : ICurrentLevelSkillDistributor
    {
        public const int MAX_REWARDS = 3;
        private readonly ILevelSkillStorage _storage;
        private CoreGamePlayContext _coreGamePlay;
        private ILevelSkill[] _cashed;


        public CurrentPlayerSkillDistributor(ILevelSkillStorage storage, CoreGamePlayContext levelWaveProvider)
        {
            _storage      = storage;
            _coreGamePlay = levelWaveProvider;
        }


        public ILevelSkill[] GenerateSkill()
        {
            LevelSkillType[] buffs = _coreGamePlay.levelWaveEntity.hasWizardShopReady ? _coreGamePlay.levelWaveEntity.wizardShopReady.LevelBuffTypes : new LevelSkillType[0];

            _cashed =  GenerateSkill(buffs);
            return _cashed;
        }

        private ILevelSkill[] GenerateSkill(LevelSkillType[] skillCandidate)
        {
            var list = new List<ILevelSkill>();
            TryAdd(list, skillCandidate); // пробуем заполнить 
            if (list.Count >= MAX_REWARDS) return list.ToArray();
            FillList(list, MAX_REWARDS);
           

            return list.ToArray();
        }

        private void FillList(List<ILevelSkill> list, int maxRewards)
        {
            var randomSkill = _storage.ToList();

            while (list.Count < maxRewards)
            {
                list.Add(randomSkill.GetRandom(true));
            }
        }

        private void TryAdd(List<ILevelSkill> list, LevelSkillType[] buffs)
        {
            for (int i = 0; i < buffs.Length; i++)
            {
                var skill = _storage.Peek(buffs[i]);
                if (skill.IsActive) continue;
                list.Add(skill);
            }
        }
    }
}