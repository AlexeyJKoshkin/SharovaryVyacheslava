namespace RoyalAxe.LevelSkill {
    public enum LevelSkillType
    {
        [LevelAdditionSettings(true)]
        FiringBlade= 10,
        [LevelAdditionSettings(true)]
        FiringFirecrackers= 20,
        FloatingShields= 30,
        HealPlayerLife= 40,
        IncreaseCriticalChance= 50,
        IncreaseDamage= 60,
        IncreasePlayerMaxLife= 70,
        IncreasePlayerSkillSpeed= 80,
        [LevelAdditionSettings(true)]
        InfectedBlood= 90,
        [LevelAdditionSettings(true)]
        Ricochet= 100,
        FireAdditionDamage= 110,
        ColdAdditionDamage= 120,
        PoisonAdditionDamage= 130,
        ChainReactionDamage= 140,
        [LevelAdditionSettings(true)]
        DoubleParallelWeapon = 150,
        [LevelAdditionSettings(true)]
        IncreasePlayerSkillUsage = 160,
        [LevelAdditionSettings(true)]
        SequentialWeaponRoll = 170,
        [LevelAdditionSettings(true)]
        ThroughDamage = 180,
        [LevelAdditionSettings(true)]
        TripleWeapon = 190,
        [LevelAdditionSettings(true)]
        IncreaseSkillUsage = 200,
    }
}