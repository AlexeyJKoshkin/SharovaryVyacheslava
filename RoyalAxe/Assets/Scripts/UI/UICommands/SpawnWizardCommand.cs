using System;
using FluentBehaviourTree;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel
{
    //Временно описаваем логику спавна волшебника дающего бафы
    public interface IWizardAtLevelFacade
    {
        void SpawnWizard(Action onDoneCallback);
    }
}