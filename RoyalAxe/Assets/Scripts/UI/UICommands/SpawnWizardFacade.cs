using System;
using Core.Data.Provider;
using FluentBehaviourTree;
using RoyalAxe.EntitasSystems;
using RoyalAxe.GameEntitas;
using RoyalAxe.Units;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace RoyalAxe.UI
{
    public class SpawnWizardFacade : IWizardAtLevelFacade
    {
        private readonly IUnitsBuilderFacade _wizardViewBuilder;
        private readonly  IUIScenarioExecutor _selectBuffWindowCommand;

        private readonly IUnitColliderDataBase _unitColliderDataBase;

        private UnitsEntity _wizard;



        public SpawnWizardFacade(IUnitsBuilderFacade wizardViewBuilder,
                                 IUnitColliderDataBase unitColliderDataBase,
                                 IUIScenarioExecutor selectBuffScenario)
        {
            _wizardViewBuilder    = wizardViewBuilder;
            _unitColliderDataBase = unitColliderDataBase;
            _selectBuffWindowCommand = selectBuffScenario;
        }

        public void SpawnWizard()
        {
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
                _selectBuffWindowCommand.ExecuteSelectBufUIScenario();
            }
        }
    }
}
