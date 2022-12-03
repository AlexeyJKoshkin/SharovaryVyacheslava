namespace RoyalAxe.LevelBuff {
    public interface ILevelSkill
    {
        LevelSkillType Type { get; }
        bool IsSingle { get; }
        bool IsActive { get; }
        void Activate();
        void DeActivate();
    }
}