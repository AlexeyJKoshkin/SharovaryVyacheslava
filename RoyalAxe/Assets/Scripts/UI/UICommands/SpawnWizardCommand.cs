using System;
using FluentBehaviourTree;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.UI
{
    //Временно описаваем логику спавна волшебника дающего бафы
    public interface IWizardAtLevelFacade
    {
        void SpawnWizard();
    }
}