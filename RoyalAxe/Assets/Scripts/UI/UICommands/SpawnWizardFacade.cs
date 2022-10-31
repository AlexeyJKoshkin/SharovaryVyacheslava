using System;
using Core.Data.Provider;
using RoyalAxe.EntitasSystems;
using RoyalAxe.GameEntitas;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RoyalAxe.CoreLevel
{
    public class SpawnWizardFacade : IWizardAtLevelFacade
    {
        private readonly IWizardViewBuilder _wizardViewBuilder;

        private readonly IShowSelectBuffWindowCommand _selectBuffWindowCommand;

        private readonly IUnitColliderDataBase _unitColliderDataBase;

        private WizardTrigger _currentTrigger;

        private Action _callback;

        public SpawnWizardFacade(IWizardViewBuilder wizardViewBuilder,
                                 IUnitColliderDataBase unitColliderDataBase,
                                 IShowSelectBuffWindowCommand selectBuffScenario)
        {
            _wizardViewBuilder    = wizardViewBuilder;
            _unitColliderDataBase = unitColliderDataBase;
            _selectBuffWindowCommand = selectBuffScenario;
        }

        public void SpawnWizard(Action onDoneCallback)
        {
            //получаем объект для спавна
            //инстанциируем его в мир
            // подписываемся
            _callback = onDoneCallback;
            _currentTrigger                     =  _wizardViewBuilder.CreateWizard();
            _currentTrigger.OnEnterTriggerEvent += WizardOnOnEnterTriggerEvent;
        }

        private void WizardOnOnEnterTriggerEvent(Collider2D collider)
        {
            var unit = _unitColliderDataBase.Get(collider);
            if (unit == null) return;
            if (unit.isPlayer)
            {
                Object.Destroy(_currentTrigger.gameObject);
                Object.Destroy(_currentTrigger);
                _selectBuffWindowCommand.ExecuteCommand();
                
            }
        }

        private void OnDoneHandler(bool isSuccess)
        {
            if(isSuccess)
                _callback?.Invoke();
        }
    }
}
