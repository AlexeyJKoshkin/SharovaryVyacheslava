using Entitas;
using RoyalAxe.Units;
using TMPro;
using UnityEngine;

namespace RoyalAxe
{
    public class CoreGameSceneUIView : MonoBehaviour, IExperienceListener, IUseCounterSkillListener, IGoldListener, IViewEntityBehaviour, ILevelNumberListener
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

        public void OnExperience(CoreGamePlayEntity entity, int value)
        {
            _expaText.text = $"Exp: {value}";
        }

        public void OnGold(CoreGamePlayEntity entity, int value)
        {
            _goldText.text = $"Gold: {value}";
        }
        
        public void OnLevelNumber(CoreGamePlayEntity entity, int number)
        {
            _levelNumberText.text = $"Level: {number}";
        }

        public void InitEntity(IEntity entity)
        {
            if (entity is CoreGamePlayEntity playEntity && playEntity.isPlayer)
            {
                playEntity.AddExperienceListener(this);
                playEntity.AddGoldListener(this);
                
                OnExperience(playEntity, playEntity.experience.Value);
                OnGold(playEntity, playEntity.gold.Value);
                return;
            }

            if (entity is CoreGamePlayEntity levelNumber && levelNumber.hasLevelNumber)
            {
                levelNumber.AddLevelNumberListener(this);
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
