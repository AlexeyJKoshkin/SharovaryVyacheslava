using System;

namespace Core.UserProfile
{
    public interface IDefaultProgressCompositeFactory
    {
        TData CreateDefault<TData>() where TData : BaseUserProgressData, new();
    }


    public interface IDefaultProgressFactory
    {
        Type ProgressType { get; }
    }

    public interface IDefaultProgressFactory<TData> : IDefaultProgressFactory where TData : BaseUserProgressData
    {
        TData CreateDefault();
    }

    public abstract class BaseDefaultProgressFactory<TData> : IDefaultProgressFactory<TData> where TData : BaseUserProgressData, new()
    {
        public abstract TData CreateDefault();


        public Type ProgressType => typeof(TData);
    }
}