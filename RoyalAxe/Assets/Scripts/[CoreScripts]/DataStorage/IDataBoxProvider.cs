using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data.Provider
{
    public interface IDataBoxProvider
    {
        void Preload();
        IEnumerable<IDataObject> All();
    }

    [Serializable]
    public abstract class AbstractDataBoxProvider<T> : IDataBoxProvider where T : IDataObject
    {
        [SerializeField] protected List<T> _items = new List<T>();
        public abstract void Preload();


        public IEnumerable<IDataObject> All()
        {
            for (int i = 0; i < _items.Count; i++) yield return _items[i];
        }
    }

    [Serializable]
    public class SubConfigDataBoxProvider<T> : AbstractDataBoxProvider<T> where T : ScriptableObject, IDataObject
    {
        [SerializeField] private DataBox _parent;

        public override void Preload() { }
    }
}