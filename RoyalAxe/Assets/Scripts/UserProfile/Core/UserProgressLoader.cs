using System;

namespace Core.UserProfile
{
    public interface IUserProgressPartSaveLoader
    {
        TProgressData LoadTo<TProgressData>(string json) where TProgressData : BaseUserProgressData, new();
        string ToJson<TProgressData>(TProgressData data) where TProgressData : BaseUserProgressData, new();
    }

    public class UserProgressLoader : IUserProgressPartSaveLoader
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IDefaultProgressCompositeFactory _defaultProgressFactory;

        public UserProgressLoader(IJsonConverter jsonConverter,
                                  IDefaultProgressCompositeFactory defaultProgressFactory)
        {
            _jsonConverter          = jsonConverter;
            _defaultProgressFactory = defaultProgressFactory;
        }


        public TData LoadTo<TData>(string json) where TData : BaseUserProgressData, new()
        {
            return Parse<TData>(json) ?? _defaultProgressFactory.CreateDefault<TData>();
        }

        public string ToJson<TData>(TData data) where TData : BaseUserProgressData, new()
        {
            data ??= _defaultProgressFactory.CreateDefault<TData>();
            return _jsonConverter.SerializeObject(data);
        }

        private TData Parse<TData>(string json) where TData : BaseUserProgressData, new()
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            try
            {
                return _jsonConverter.Deserialize<TData>(json);
            }
            catch (Exception e)
            {
                HLogger.LogException(e);
                return _defaultProgressFactory.CreateDefault<TData>();
            }
        }
    }
}