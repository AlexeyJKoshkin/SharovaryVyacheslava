using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.UserProfile
{
    public class DefaultProgressFactoryComposite : IDefaultProgressCompositeFactory
    {
        private readonly Dictionary<Type, IDefaultProgressFactory> _dic;

        public DefaultProgressFactoryComposite(IReadOnlyList<IDefaultProgressFactory> allFactory)
        {
            _dic = allFactory.ToDictionary(o => o.ProgressType, o => o);
        }

        public TData CreateDefault<TData>() where TData : BaseUserProgressData, new()
        {
            var key = typeof(TData);
            return _dic.TryGetValue(key, out var factory) ? ((IDefaultProgressFactory<TData>) factory).CreateDefault() : new TData();
        }
    }
}