using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RoyalAxe.GameEntitas
{
    [DontGenerate]
    public abstract class AbstractCollectionComponent<TCollection, TValue> : IEnumerable<TValue>, IComponent where TCollection : class, IEnumerable<TValue>, ICollection<TValue>
    {
        public int Count => Collection.Count;

        public TCollection Collection;

        public bool Contains(TValue item)
        {
            return Collection.Contains(item);
        }

        public TCollection Add(TValue item)
        {
            Collection.Add(item);
            return Collection;
        }

        public TCollection Clear()
        {
            Collection.Clear();
            return Collection;
        }

        public TCollection Remove(TValue item)
        {
            Collection.Remove(item);
            return Collection;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}