using Core;

namespace RoyalAxe.LevelSkill
{
    public abstract class AbstractPlayerSkillStrategy : ILevelSkill
    {
        public abstract LevelSkillType Type { get; }
        public virtual bool IsSingle => false;
        public bool IsActive => _isActivated;
        private bool _isActivated;
        
        public void Activate()
        {
            if (_isActivated)
            {
                HLogger.LogError($"{Type} IsSingle : {IsSingle} is alreadyActivated");
                return;
            }
            DoLevelPowerActivate();
            _isActivated = IsSingle;
        }
        
        public void DeActivate()
        {
            if (!_isActivated)
            {
                HLogger.LogError($"{Type} IsSingle : {IsSingle} is unactive");
                return;
            }
            DoLevelPowerDeActivate();
            _isActivated = false;
        }

        public abstract void DoLevelPowerActivate();

        public abstract void DoLevelPowerDeActivate();
        
        
    }

    /// <summary>
    /// todo: подумать о создании билдера стратегии. чтобы не паредавать слишком дофига параметров в конструктор
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractPlayerSkillStrategy<T> : AbstractPlayerSkillStrategy where T : BaseLevelSkillSettings
    {
        public sealed override LevelSkillType Type => _settings.Type;
        public struct StrategySettings
        {
            public LevelSkillType Type;
            public bool IsSingle;
            public T Settings;
        }

        protected T Settings => _settings.Settings;
        private readonly StrategySettings _settings;
        public sealed override bool IsSingle => _settings.IsSingle;

        public AbstractPlayerSkillStrategy(ILevelBuffSettingCompositeProvider provider)
        {
            _settings = provider.GetSettings<T>();
        }

    }
}