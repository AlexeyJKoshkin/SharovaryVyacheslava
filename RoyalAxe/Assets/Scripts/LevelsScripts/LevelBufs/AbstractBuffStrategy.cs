using System;
using Core;
using RoyalAxe.EntitasSystems;

namespace RoyalAxe.LevelBuff
{
    public abstract class AbstractBuffStrategy : ILevelBuff
    {
   
        public abstract LevelBuffType Type { get; }
        public virtual bool IsSingle => false;
        private bool _isActivated;
        
        public void Activate()
        {
            if (_isActivated)
            {
                HLogger.LogError($"{Type} IsSingle : {IsSingle} is alreadyActivated");
                return;
            }
            DoBuffStrategyActivate();
            _isActivated = IsSingle;
        }
        
        public virtual void DoBuffStrategyActivate()
        {
            HLogger.LogError("Надо реализовать");
        }
        
    }

    /// <summary>
    /// todo: подумать о создании билдера стратегии. чтобы не паредавать слишком дофига параметров в конструктор
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractBuffStrategy<T> : AbstractBuffStrategy where T : BaseLevelBuffSettings
    {
        public sealed override LevelBuffType Type => _settings.Type;
        public struct StrategySettings
        {
            public LevelBuffType Type;
            public bool IsSingle;
            public T Settings;
        }

        protected T Settings => _settings.Settings;
        private StrategySettings _settings;
        public sealed override bool IsSingle => _settings.IsSingle;

        public AbstractBuffStrategy(ILevelBuffSettingCompositeProvider provider)
        {
            _settings = provider.GetSettings<T>();
        }

    }
}