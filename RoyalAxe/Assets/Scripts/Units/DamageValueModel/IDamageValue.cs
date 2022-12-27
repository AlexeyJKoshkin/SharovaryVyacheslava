namespace RoyalAxe {
    public interface IDamageValue
    {
        float Damage { get; }
        float CriticalDamage { get; set; }

        void IncreaseDamage(float delta);
    }
}