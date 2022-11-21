using System;

namespace Core.UserProfile
{
    public interface IUserProgressPartFactory<TProgressData> where TProgressData : BaseUserProgressData
    {
        TProgressData LoadTo(string json);
        string ToJson(TProgressData data);
    }

    public class UserProgressLoader<TData> : IUserProgressPartFactory<TData> where TData : BaseUserProgressData 
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IDefaultProgressFactory<TData> _defaultProgressFactory;
        public UserProgressLoader(IJsonConverter jsonConverter,
                                  IDefaultProgressFactory<TData> defaultProgressFactory
        )
        {
            _jsonConverter          = jsonConverter;
            _defaultProgressFactory = defaultProgressFactory;
        }


        public TData LoadTo(string json)
        {
            return  Parse(json) ?? _defaultProgressFactory.CreateDefault();
        }

        public string ToJson(TData data)
        {
            data ??= _defaultProgressFactory.CreateDefault();
            return _jsonConverter.SerializeObject(data);

        }

        private TData Parse(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            try
            {
                return _jsonConverter.Deserialize<TData>(json);
            }
            catch (Exception e)
            {
                HLogger.LogException(e);
                return _defaultProgressFactory.CreateDefault();
            }
        }
    }
}
