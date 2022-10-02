#region

using System;
using System.Collections.Generic;

#endregion

namespace Core.Data.Provider
{
    public interface IDataBox<T> : IDataBox where T : class, IDataObject
    {
        IReadOnlyCollection<T> Collection { get; }
        T this[int id] { get; }
    }

    public interface IDataBox
    {
        Type ObjectType { get; }

        int Count { get; }

        //у юнити колбэки ISerializationCallbackReceiver приходят ассинхронно и чтобы создать словарь данных приходится вызывать метод при регистрации в хранилищ
        // void Reload();
        void Reload();
    }
}