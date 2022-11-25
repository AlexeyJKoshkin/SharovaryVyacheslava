using System.Collections.Generic;
using Core.Data.Provider;

namespace Core.Configs
{
    public interface IJsonConfigsModelsLoader
    {
        IEnumerable<T> Load<T>(string path= null) where T : class;
        T LoadSingle<T>(string path= null) where T : class;
    }

    public interface IJsonConfigModelsOperation : IJsonConfigsModelsLoader
    {
        void Save<T>(IEnumerable<T> data);
        void Save<T>(T data);
    }
}