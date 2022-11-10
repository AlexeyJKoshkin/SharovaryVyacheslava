using System;
using Core.Data.Provider;
using RoyalAxe.EntitasSystems;
using RoyalAxe.GameEntitas;
using RoyalAxe.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RoyalAxe.CoreLevel
{
    public class SpawnWizardFacade : IWizardAtLevelFacade
    {
        private readonly IUnitsBuilderFacade _wizardViewBuilder;

        private readonly IShowSelectBuffWindowCommand _selectBuffWindowCommand;

        private readonly IUnitColliderDataBase _unitColliderDataBase;

        private UnitsEntity _wizard;

        private Action _callback;

        public SpawnWizardFacade(IUnitsBuilderFacade wizardViewBuilder,
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
            
            _wizard = _wizardViewBuilder.CreateWizardShowUnit();
            _wizard.unitsView.Get<WizardShopUnitView>().OnEnterTriggerEvent += WizardOnOnEnterTriggerEvent;
        }

        private void WizardOnOnEnterTriggerEvent(Collider2D collider)
        {
            var unit = _unitColliderDataBase.Get(collider);
            if (unit == null) return;
            if (unit.isPlayer)
            {
                _wizard.isDestroyUnit = true;
                _selectBuffWindowCommand.ExecuteCommand(OnDoneHandler);
            }
        }

        private void OnDoneHandler(bool isSuccess)
        {
            if(isSuccess)
                _callback?.Invoke();
        }
    }
}
