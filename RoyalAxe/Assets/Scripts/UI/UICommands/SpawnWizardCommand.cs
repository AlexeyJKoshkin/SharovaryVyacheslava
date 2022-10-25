using System;
using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public interface IWizardAtLevelFacade
    {
        void SpawnWizard(Action onDoneCallback);
    }

    //Временно описаваем логику спавна волшебника дающего бафы
}