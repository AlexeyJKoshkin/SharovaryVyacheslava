using System.Collections;
using System.Collections.Generic;
using Entitas;
using RoyalAxe.Units;
using TMPro;
using UnityEngine;

namespace RoyalAxe
{
    public class CoreGameSceneUIView : MonoBehaviour, IExperienceListener, IUseCounterSkillListener, IGoldListener, IViewEntityBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _expaText;
        
        [SerializeField]
        private TextMeshProUGUI _goldText;

        [SerializeField] private TextMeshProUGUI _axeCounterText;

        public void OnUseCounterSkill(SkillEntity entity, int currentValue, int maxValue)
        {
            _axeCounterText.text = $"{currentValue}/{maxValue}";
        }

        public void OnExperience(CoreGamePlayEntity entity, int value)
        {
            _expaText.text = value.ToString();
        }

        public void OnGold(CoreGamePlayEntity entity, int value)
        {
            _goldText.text = value.ToString();
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

            if (entity is UnitsEntity unitsEntity && unitsEntity.isPlayer)
            {
                var skill = unitsEntity.unitActiveSkill.SkillEntity;
                skill.AddUseCounterSkillListener(this);
                
                OnUseCounterSkill(skill, skill.useCounterSkill.CurrentValue, skill.useCounterSkill.MaxValue);
            }
        }
    }
}
