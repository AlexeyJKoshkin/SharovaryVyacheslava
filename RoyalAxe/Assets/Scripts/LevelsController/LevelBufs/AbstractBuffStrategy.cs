using System;
using Core;

namespace RoyalAxe.LevelBuff
{
    public abstract class AbstractBuffStrategy : ILevelBuff
    {
        public LevelBuffType Type;
        public virtual bool IsSingle { get; }

        public virtual void Activate()
        {
            HLogger.LogError("Надо реализовать");
        }
    }
}