using System;
using Core.Data.Provider;

namespace RoyalAxe.LevelBuff
{
    /*
     * т.к. каждый баф настраивается отдельно. То заводим большой серриализуемый конфиг
     * Влом писать json конвертер пущай все хранится в одном месте. если что потом переделаем
     * да и создавать бафы будет проще. просто передавать этот композит как зависимость
     */
    [Serializable]
    public class LevelBuffSettingsComposite
    {
        public FiringBladeBuffSettings FiringBladeBuffSetting;
        public FiringFirecrackersBuffSettings FiringFirecrackersBuffSetting;
        public FloatingShieldsBuffSettings FloatingShieldsBuffSetting;
        public HealPlayerLifeBuffSettings HealPlayerLifeBuffSetting;
        public IncreaseCriticalChanceBuffSettings IncreaseCriticalChanceBuffSetting;
        public IncreaseDamageBuffSettings IncreaseDamageBuffSetting;
        public IncreasePlayerMaxLifeBuffSettings IncreasePlayerMaxLifeBuffSetting;
        public IncreasePlayerSkillSpeedBuffSettings IncreasePlayerSkillSpeedBuffSetting;
        public InfectedBloodBuffSettings InfectedBloodBuffSetting;
        public RicochetBuffSettings RicochetBuffSetting;

        public FireAdditionDamageBuffSettings FireAdditionDamageBuffSettings;
        public ColdAdditionDamageBuffSettings ColdAdditionDamageBuffSettings;
        public PoisonAdditionDamageBuffSettings PoisonAdditionDamageBuffSettings;

        public ChainReactionDamageBuffSettings ChainReactionDamageBuffSettings;
    }
}