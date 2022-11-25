using System;
using RoyalAxe.CharacterStat;
using Sirenix.OdinInspector;

namespace RoyalAxe.Configs
{
    [Serializable]
    public class EntityStatConfig
    {
        public CharacterStatTypeParameters Config;
        public CharacterStatValue Value;
        [OnValueChanged("UpdateValueByParams")]
        public bool UseFromConfig;

        private void UpdateValueByParams()
        {
            if (UseFromConfig)
            {
                Value = Config.DefaultValue;
            }
        }
    }
}