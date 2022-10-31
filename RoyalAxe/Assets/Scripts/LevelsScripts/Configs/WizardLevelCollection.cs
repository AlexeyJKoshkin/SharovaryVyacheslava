using System;
using System.Collections.Generic;
using Core.Data.Provider;

namespace RoyalAxe.CoreLevel {
    [Serializable]
    public class WizardLevelCollection : IDataObject
    {
        public string UniqueID { get; set; }

        public List<WizardShopSettings> Settings = new List<WizardShopSettings>();

        public WizardShopSettings GetByLevel(int wizardLevel)
        {
            wizardLevel--; // уровнь всегда на 1 больше чем индекс
            if (wizardLevel < Settings.Count)
            {
                return Settings[wizardLevel];
            }

            return new WizardShopSettings();
        }
    }
}