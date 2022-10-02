using System;

namespace RoyalAxe.LevelBuff
{
    public abstract class AbstractBuffStrategy : ILevelBuff
    {
        public LevelBuffType Type;
        public virtual bool IsSingle { get; }

        public virtual void Activate() { throw new NotImplementedException();}
    }
}