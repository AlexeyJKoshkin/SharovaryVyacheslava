using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.Parser;

namespace RoyalAxe.LevelSkill
{
    /*
     * т.к. каждый баф настраивается отдельно. То заводим большой серриализуемый конфиг
     * Влом писать json конвертер пущай все хранится в одном месте. если что потом переделаем
     * да и создавать бафы будет проще. просто передавать этот композит как зависимость
     */
    [Serializable]
    public class LevelBuffSettingsComposite
    {
        [GoogleSheetPageName("FiringBladeBuffSettings")]
        public FiringBladeSkillSettings FiringBlade = new FiringBladeSkillSettings();
        [GoogleSheetPageName("FiringFirecrackersBuffSettings")]
        public FiringFirecrackersSkillSettings FiringFirecrackers = new FiringFirecrackersSkillSettings();
        [GoogleSheetPageName("FloatingShieldsBuffSettings")]
        public FloatingShieldsSkillSettings FloatingShields = new FloatingShieldsSkillSettings();
        [GoogleSheetPageName("HealPlayerLifeBuffSettings")]
        public HealPlayerLifeSkillSettings HealPlayerLife = new HealPlayerLifeSkillSettings();
        [GoogleSheetPageName("IncreaseCriticalChanceBuffSettings")]
        public IncreaseCriticalChanceSkillSettings IncreaseCriticalChance = new IncreaseCriticalChanceSkillSettings();
        [GoogleSheetPageName("IncreaseDamageBuffSettings")]
        public IncreaseDamageSkillSettings IncreaseDamage = new IncreaseDamageSkillSettings();
        [GoogleSheetPageName("IncreasePlayerMaxLifeBuffSettings")]
        public IncreasePlayerMaxLifeSkillSettings IncreasePlayerMaxLife = new IncreasePlayerMaxLifeSkillSettings();
        [GoogleSheetPageName("IncreasePlayerSkillSpeedBuffSettings")]
        public IncreasePlayerSkillSpeedSkillSettings IncreasePlayerSkillSpeed = new IncreasePlayerSkillSpeedSkillSettings();
        [GoogleSheetPageName("InfectedBloodBuffSettings")]
        public InfectedBloodSkillSettings InfectedBlood = new InfectedBloodSkillSettings();
        [GoogleSheetPageName("RicochetBuffSettings")]
        public RicochetSkillSettings Ricochet = new RicochetSkillSettings();
        [GoogleSheetPageName("FireAdditionalDamageBuffSettings")]
        public FireAdditionalDamageSkillSettings FireAdditionalDamage = new FireAdditionalDamageSkillSettings();
        [GoogleSheetPageName("ColdAdditionalDamageBuffSettings")]
        public ColdAdditionalDamageSkillSettings ColdAdditionalDamage = new ColdAdditionalDamageSkillSettings();
        [GoogleSheetPageName("PoisonAdditionalDamageBuffSettings")]
        public PoisonAdditionalDamageSkillSettings PoisonAdditional = new PoisonAdditionalDamageSkillSettings();
        [GoogleSheetPageName("ChainReactionDamageBuffSettings")]
        public ChainReactionDamageSkillSettings ChainReactionDamage =new ChainReactionDamageSkillSettings();
        [GoogleSheetPageName("IncreasePlayerSkillUsageBuffSettings")]
        public IncreasePlayerSkillUsageSkillSettings IncreasePlayerSkillUsage =new IncreasePlayerSkillUsageSkillSettings();
        
        

        public IEnumerable<BaseLevelSkillSettings> AllSettings()
        {
            yield return FiringBlade;
            yield return FiringFirecrackers;
            yield return FloatingShields;
            yield return Ricochet;
            yield return PoisonAdditional;
            yield return ColdAdditionalDamage;
            yield return FireAdditionalDamage;
            yield return InfectedBlood;
            yield return IncreasePlayerSkillSpeed;
            yield return IncreasePlayerMaxLife;
            yield return IncreaseDamage;
            yield return IncreaseCriticalChance;
            yield return HealPlayerLife;
            yield return ChainReactionDamage;
            yield return IncreasePlayerSkillUsage;
        }
    }
}