namespace RoyalAxe.CharacterStat
{
    public struct HitDamageInfo
    {
        public float HitValue;
        public DamageType DamageType;
        public bool IsCritical;
    }

    public class DamageInfluenceData
    {
        public float Damage;
        public DamageType ElementalDamageType;

        public DamageInfluenceData()
        {
            Damage = 0;
            ElementalDamageType = DamageType.Physical;
        }
        public DamageInfluenceData(float damage, DamageType type)
        {
            Damage = damage;
            ElementalDamageType = type;
        }
    }

    public class PeriodicDamageInfluenceData : DamageInfluenceData
    {
        public float MagicDuration;
        public float DamageCooldown;
      
    }
}