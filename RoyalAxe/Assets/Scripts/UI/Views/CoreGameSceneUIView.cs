using Core.UserProfile;
using Entitas;
using RoyalAxe.Units;
using TMPro;
using UnityEngine;

namespace RoyalAxe
{
    public class CoreGameSceneUIView : MonoBehaviour, IEarnedExperienceListener, IUseCounterSkillListener, IEarnedGoldListener, IViewEntityBehaviour, ICurrentLevelInfoListener
    {
        [SerializeField]
        private TextMeshProUGUI _expaText;
        [SerializeField]
        private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _axeCounterText;
        [SerializeField]
        private TextMeshProUGUI _levelNumberText;

        public void OnUseCounterSkill(SkillEntity entity, int currentValue, int maxValue)
        {
            _axeCounterText.text = $"Axe {currentValue}/{maxValue}";
        }
        
        public void OnEarnedExperience(CoreGamePlayEntity entity, int value)
        {
            _expaText.text = $"Exp: {value}";
        }

        public void OnEarnedGold(CoreGamePlayEntity entity, int value)
        {
            _goldText.text = $"Gold: {value}";
        }
        public void OnCurrentLevelInfo(CoreGamePlayEntity entity, LastLevel level)
        {
            _levelNumberText.text = $"Level: {level.LevelNumber}";
        }
        
        

        public void InitEntity(IEntity entity)
        {
            if (entity is CoreGamePlayEntity playEntity && playEntity.isPlayer)
            {
                playEntity.AddEarnedExperienceListener(this);
                playEntity.AddEarnedGoldListener(this);
                
                OnEarnedExperience(playEntity, playEntity.earnedExperience.Value);
                OnEarnedGold(playEntity, playEntity.earnedGold.Value);
                return;
            }

            if (entity is CoreGamePlayEntity levelNumber && levelNumber.hasCurrentLevelInfo)
            {
                levelNumber.AddCurrentLevelInfoListener(this);
                return;
            }

            if (entity is UnitsEntity unitsEntity && unitsEntity.isPlayer)
            {
                var skill = unitsEntity.unitActiveSkill.SkillEntity;
                skill.AddUseCounterSkillListener(this);
                
                OnUseCounterSkill(skill, skill.useCounterSkill.CurrentValue, skill.useCounterSkill.MaxValue);
            }
        }


     
    }
}
