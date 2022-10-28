using System;
using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    //Временно описаваем логику спавна волшебника дающего бафы
    public interface IWizardAtLevelFacade
    {
        void SpawnWizard(Action onDoneCallback);
    }
}