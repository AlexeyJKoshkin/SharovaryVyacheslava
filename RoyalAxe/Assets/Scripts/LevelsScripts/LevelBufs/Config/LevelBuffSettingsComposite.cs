using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.Parser;

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
        [GoogleSheetPageName]
        public FiringBladeBuffSettings FiringBladeBuffSettings = new FiringBladeBuffSettings();
        [GoogleSheetPageName]
        public FiringFirecrackersBuffSettings FiringFirecrackersBuffSettings = new FiringFirecrackersBuffSettings();
        [GoogleSheetPageName]
        public FloatingShieldsBuffSettings FloatingShieldsBuffSettings = new FloatingShieldsBuffSettings();
        [GoogleSheetPageName]
        public HealPlayerLifeBuffSettings HealPlayerLifeBuffSettings = new HealPlayerLifeBuffSettings();
        [GoogleSheetPageName]
        public IncreaseCriticalChanceBuffSettings IncreaseCriticalChanceBuffSettings = new IncreaseCriticalChanceBuffSettings();
        [GoogleSheetPageName]
        public IncreaseDamageBuffSettings IncreaseDamageBuffSettings = new IncreaseDamageBuffSettings();
        [GoogleSheetPageName]
        public IncreasePlayerMaxLifeBuffSettings IncreasePlayerMaxLifeBuffSettings = new IncreasePlayerMaxLifeBuffSettings();
        [GoogleSheetPageName]
        public IncreasePlayerSkillSpeedBuffSettings IncreasePlayerSkillSpeedBuffSettings = new IncreasePlayerSkillSpeedBuffSettings();
        [GoogleSheetPageName]
        public InfectedBloodBuffSettings InfectedBloodBuffSettings = new InfectedBloodBuffSettings();
        [GoogleSheetPageName]
        public RicochetBuffSettings RicochetBuffSettings = new RicochetBuffSettings();
        [GoogleSheetPageName]
        public FireAdditionalDamageBuffSettings FireAdditionalDamageBuffSettings = new FireAdditionalDamageBuffSettings();
        [GoogleSheetPageName]
        public ColdAdditionalDamageBuffSettings ColdAdditionalDamageBuffSettings = new ColdAdditionalDamageBuffSettings();
        [GoogleSheetPageName]
        public PoisonAdditionalDamageBuffSettings PoisonAdditionalDamageBuffSettings = new PoisonAdditionalDamageBuffSettings();
        [GoogleSheetPageName]
        public ChainReactionDamageBuffSettings ChainReactionDamageBuffSettings =new ChainReactionDamageBuffSettings();

        public IEnumerable<BaseLevelBuffSettings> AllSettings()
        {
            yield return FiringBladeBuffSettings;
            yield return FiringFirecrackersBuffSettings;
            yield return FloatingShieldsBuffSettings;
            yield return RicochetBuffSettings;
            yield return PoisonAdditionalDamageBuffSettings;
            yield return ColdAdditionalDamageBuffSettings;
            yield return FireAdditionalDamageBuffSettings;
            yield return InfectedBloodBuffSettings;
            yield return IncreasePlayerSkillSpeedBuffSettings;
            yield return IncreasePlayerMaxLifeBuffSettings;
            yield return IncreaseDamageBuffSettings;
            yield return IncreaseCriticalChanceBuffSettings;
            yield return HealPlayerLifeBuffSettings;
            yield return ChainReactionDamageBuffSettings;
        }
    }
}