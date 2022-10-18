using RoyalAxe.EntitasSystems;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
   

    //Временно описаваем логику спавна волшебника дающего бафы
    public class SpawnWizardCommand
    {
        private readonly IWizardViewBuilder _wizardViewBuilder;

        private readonly IShowBuffCommand _showBuffCommand;

        private readonly IUnitColliderDataBase _unitColliderDataBase;

        private WizardTrigger _currentTrigger;

        public SpawnWizardCommand(IWizardViewBuilder wizardViewBuilder,
                                  IShowBuffCommand showBuffCommand,
                                  IUnitColliderDataBase unitColliderDataBase)
        {
            _wizardViewBuilder    = wizardViewBuilder;
            _showBuffCommand      = showBuffCommand;
            _unitColliderDataBase = unitColliderDataBase;
        }

        void SpawnWizard()
        {
            //получаем объект для спавна
            //инстанциируем его в мир
            // подписываемся

            _currentTrigger                     =  _wizardViewBuilder.CreateWizard();
            _currentTrigger.OnEnterTriggerEvent += WizardOnOnEnterTriggerEvent;
        }

        private void WizardOnOnEnterTriggerEvent(Collider2D collider)
        {
            var unit = _unitColliderDataBase.Get(collider);
            if (unit == null) return;
            if (unit.isPlayer)
            {
                GameObject.Destroy(_currentTrigger.gameObject);
                GameObject.Destroy(_currentTrigger);
                _showBuffCommand.DoShowExpBuffs();
            }
        }
    }
}