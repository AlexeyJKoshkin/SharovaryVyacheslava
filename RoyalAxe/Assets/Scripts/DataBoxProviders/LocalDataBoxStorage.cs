#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Core.Data.Provider
{
    public class LocalDataBoxStorage : IDataStorage
    {
        public int CountProviders => Members.Count;
        protected readonly Dictionary<Type, IDataBox> Members;

        public LocalDataBoxStorage(IReadOnlyList<IDataBox> alDataBoxes)
        {
            Members = new Dictionary<Type, IDataBox>();
            foreach (var box in alDataBoxes) Add(box);
        }

        public void Add(IDataBox box)
        {
            Members.Add(box.ObjectType, box);
            box.Reload();
        }

        public void Remove(IDataBox box)
        {
            Members.Remove(box.ObjectType);
        }


        protected IDataBox<T> GetMember<T>() where T : class, IDataObject
        {
            IDataBox result = null;
            if (Members.TryGetValue(typeof(T), out result))
            {
                return result as IDataBox<T>;
            }

            throw new ArgumentException(string.Format("Provider for {0} not registered", typeof(T)));
        }

        #region DataProvider

        public T First<T>() where T : class, IDataObject
        {
            return GetMember<T>()?.Collection.FirstOrDefault();
        }

        public T First<T>(Predicate<T> predicate) where T : class, IDataObject
        {
            return GetMember<T>()?.Collection.FirstOrDefault(predicate.Invoke);
        }

        public IReadOnlyCollection<T> All<T>() where T : class, IDataObject
        {
            return GetMember<T>().Collection;
        }

       

        public T[] PackById<T>(params string[] ids) where T : class, IDataObject
        {
            if (ids == null || ids.Length == 0)
            {
                return null;
            }

            var member = GetMember<T>();
            return ids.Select(e => member[e.GetHashCode()]).Where(o => o != default).ToArray();
        }

        public int Count<T>() where T : class, IDataObject
        {
            return GetMember<T>().Collection.Count;
        }
        
        public T ById<T>(int indId) where T : class, IDataObject
        {
            var member = GetMember<T>();
            var result = member[indId];
            return result;
        }

        public T ById<T>(string id) where T : class, IDataObject
        {
            
            int indId = id.GetHashCode();
            var result = ById<T>(indId);
            #if UNITY_EDITOR
            if (result == null)
            {
                HLogger.LogError($"Not found {typeof(T).Name} with id: {id}");
            }
            #endif
            return result;
        }

        #endregion DataProvider

        public IEnumerator<IDataBox> GetEnumerator()
        {
            return Members.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}