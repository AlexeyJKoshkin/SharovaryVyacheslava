namespace RoyalAxe.Units.Stats {
    public interface IUnitDamageApplierFactory
    {
        IPeriodicInfluenceApplier CreatePeriodicDamage(SkillConfigDef.Damage periodicDamageInfluenceData);
     //   IEntityBuff CreateElementalDamageBuf(UnitsEntity attacker, SkillConfigDef.Damage damage);

        InfluenceApplierComposite CreateComposite();
        IInfluenceApplier CreateAdditionalDamageApplier(SkillConfigDef.Damage settingsDamage);
    }
}