using System;
using System.Collections.Generic;
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
        public FiringBladeBuffSettings FiringBladeBuffSetting = new FiringBladeBuffSettings();
        public FiringFirecrackersBuffSettings FiringFirecrackersBuffSetting = new FiringFirecrackersBuffSettings();
        public FloatingShieldsBuffSettings FloatingShieldsBuffSetting = new FloatingShieldsBuffSettings();
        public HealPlayerLifeBuffSettings HealPlayerLifeBuffSetting = new HealPlayerLifeBuffSettings();
        public IncreaseCriticalChanceBuffSettings IncreaseCriticalChanceBuffSetting = new IncreaseCriticalChanceBuffSettings();
        public IncreaseDamageBuffSettings IncreaseDamageBuffSetting = new IncreaseDamageBuffSettings();
        public IncreasePlayerMaxLifeBuffSettings IncreasePlayerMaxLifeBuffSetting = new IncreasePlayerMaxLifeBuffSettings();
        public IncreasePlayerSkillSpeedBuffSettings IncreasePlayerSkillSpeedBuffSetting = new IncreasePlayerSkillSpeedBuffSettings();
        public InfectedBloodBuffSettings InfectedBloodBuffSetting = new InfectedBloodBuffSettings();
        public RicochetBuffSettings RicochetBuffSetting = new RicochetBuffSettings();
        
        public FireAdditionalDamageBuffSettings fireAdditionalDamageBuffSettings = new FireAdditionalDamageBuffSettings();
        public ColdAdditionalDamageBuffSettings coldAdditionalDamageBuffSettings = new ColdAdditionalDamageBuffSettings();
        public PoisonAdditionalDamageBuffSettings poisonAdditionalDamageBuffSettings = new PoisonAdditionalDamageBuffSettings();
        public ChainReactionDamageBuffSettings ChainReactionDamageBuffSettings =new ChainReactionDamageBuffSettings();

        public IEnumerable<BaseLevelBuffSettings> AllSettings()
        {
            yield return FiringBladeBuffSetting;
            yield return FiringFirecrackersBuffSetting;
            yield return FloatingShieldsBuffSetting;
            yield return HealPlayerLifeBuffSetting;
            yield return IncreaseCriticalChanceBuffSetting;
            yield return IncreaseDamageBuffSetting;
            yield return IncreasePlayerMaxLifeBuffSetting;
            yield return IncreasePlayerSkillSpeedBuffSetting;
            yield return InfectedBloodBuffSetting;
            yield return RicochetBuffSetting;
            yield return fireAdditionalDamageBuffSettings;
            yield return coldAdditionalDamageBuffSettings;
            yield return poisonAdditionalDamageBuffSettings;
            yield return ChainReactionDamageBuffSettings;
        }
    }
}