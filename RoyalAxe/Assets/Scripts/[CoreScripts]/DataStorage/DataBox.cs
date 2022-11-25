#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Core.Data.Provider
{
    [Serializable]
    public abstract class DataBox<T> : DataBox, IDataBox<T> where T : class, IDataObject
    {
        protected Dictionary<int, T> _dicItems = new Dictionary<int, T>();
        public T this[int id] => _dicItems.TryGetValue(id, out var result) ? result : default;
        [SerializeField] protected List<T> _collection = new List<T>();

        public override string CollectionName => $"{ObjectType.Name}_Data";
        public override Type ObjectType => typeof(T);
        public override int Count => _collection.Count;

        public override void OnBeforeSerialize()
        {
            _collection.RemoveAll(o => ReferenceEquals(o, null));
        }


        public override void Reload()
        {
            _dicItems.Clear();
            _collection.ForEach(e => { _dicItems.Add(e.UniqueID.GetHashCode(), e); });
        }

        public IReadOnlyCollection<T> Collection => _dicItems.Values;
    }

    [Flags]
    public enum DataBoxProviderType
    {
        None = 0,
        SubConfigs = 1 << 1,
        Folders = 1 << 2
    }

    [Serializable]
    public abstract class DataBox : ScriptableObject, IDataBox, IDataObject, ISerializationCallbackReceiver
    {
        public virtual string CollectionName => "Empty";

        public virtual Type ObjectType => null;

        public virtual int Count => 0;

        public abstract void Reload();

        public virtual void OnBeforeSerialize() { }

        public virtual void OnAfterDeserialize() { }
        public string UniqueID => name;

        public static int DataobjComparer(IDataObject o1, IDataObject o2)
        {
            return string.Compare(o1.UniqueID, o2.UniqueID);
        }
    }
}