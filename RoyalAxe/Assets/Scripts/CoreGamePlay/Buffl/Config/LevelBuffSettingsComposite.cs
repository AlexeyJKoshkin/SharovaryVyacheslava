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
        [GoogleSheetPageName(GoogleSheetPageNameAttribute.FieldNameType.SameAsTypeName)]
        public FiringBladeBuffSettings FiringBlade = new FiringBladeBuffSettings();
        [GoogleSheetPageName]
        public FiringFirecrackersBuffSettings FiringFirecrackers = new FiringFirecrackersBuffSettings();
        [GoogleSheetPageName]
        public FloatingShieldsBuffSettings FloatingShields = new FloatingShieldsBuffSettings();
        [GoogleSheetPageName]
        public HealPlayerLifeBuffSettings HealPlayerLife = new HealPlayerLifeBuffSettings();
        [GoogleSheetPageName]
        public IncreaseCriticalChanceBuffSettings IncreaseCriticalChance = new IncreaseCriticalChanceBuffSettings();
        [GoogleSheetPageName]
        public IncreaseDamageBuffSettings IncreaseDamage = new IncreaseDamageBuffSettings();
        [GoogleSheetPageName]
        public IncreasePlayerMaxLifeBuffSettings IncreasePlayerMaxLife = new IncreasePlayerMaxLifeBuffSettings();
        [GoogleSheetPageName]
        public IncreasePlayerSkillSpeedBuffSettings IncreasePlayerSkillSpeed = new IncreasePlayerSkillSpeedBuffSettings();
        [GoogleSheetPageName]
        public InfectedBloodBuffSettings InfectedBlood = new InfectedBloodBuffSettings();
        [GoogleSheetPageName]
        public RicochetBuffSettings Ricochet = new RicochetBuffSettings();
        [GoogleSheetPageName]
        public FireAdditionalDamageBuffSettings FireAdditionalDamage = new FireAdditionalDamageBuffSettings();
        [GoogleSheetPageName]
        public ColdAdditionalDamageBuffSettings ColdAdditionalDamage = new ColdAdditionalDamageBuffSettings();
        [GoogleSheetPageName]
        public PoisonAdditionalDamageBuffSettings PoisonAdditional = new PoisonAdditionalDamageBuffSettings();
        [GoogleSheetPageName]
        public ChainReactionDamageBuffSettings ChainReactionDamage =new ChainReactionDamageBuffSettings();
        [GoogleSheetPageName]
        public IncreasePlayerSkillUsageBuffSettings IncreasePlayerSkillUsage =new IncreasePlayerSkillUsageBuffSettings();
        
        

        public IEnumerable<BaseLevelBuffSettings> AllSettings()
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