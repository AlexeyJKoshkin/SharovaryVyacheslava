using System.Collections.Generic;
using Entitas;
using UnityEngine;
using VContainer.Unity;

namespace RoyalAxe.EntitasSystems
{
    public interface IUnitColliderDataBase
    {
        UnitsEntity Get(Collider2D collider2D);
        Collider2D Get(UnitsEntity unit);
    }

    public class UnitColliderDataBaseSystem : IUnitColliderDataBase, IInitializeSystem, ITearDownSystem
    {
        private readonly Dictionary<Collider2D, UnitsEntity> _units = new Dictionary<Collider2D, UnitsEntity>();
        private readonly Dictionary<UnitsEntity, Collider2D> _colliders = new Dictionary<UnitsEntity, Collider2D>();

        private readonly IGroup<UnitsEntity> _group;

        public UnitColliderDataBaseSystem(UnitsContext context)
        {
            _group = context.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.UnitsView, UnitsMatcher.UnitPhysicCollider));
        }

        public UnitsEntity Get(Collider2D collider2D)
        {
            return Get(_units, collider2D);
        }

        public Collider2D Get(UnitsEntity unit)
        {
            return Get(_colliders, unit);
        }

        private TResult Get<TKey, TResult>(Dictionary<TKey, TResult> dic, TKey key) where TKey : class where TResult : class
        {
            TResult result = default;
            if (key != null)
            {
                dic.TryGetValue(key, out result);
            }

            return result;
        }

        public void Initialize()
        {
            _group.OnEntityAdded   += GroupOnOnEntityAdded;
            _group.OnEntityRemoved += GroupOnOnEntityRemoved;
        }

        public void TearDown()
        {
            _group.OnEntityRemoved -= GroupOnOnEntityRemoved;
            _group.OnEntityAdded   -= GroupOnOnEntityAdded;
            _colliders.Clear();
            _units.Clear();
        }

        private void GroupOnOnEntityRemoved(IGroup<UnitsEntity> group, UnitsEntity entity, int index, IComponent component)
        {
            var collider = _colliders[entity];
            _colliders.Remove(entity);
            _units.Remove(collider);
        }

        private void GroupOnOnEntityAdded(IGroup<UnitsEntity> group, UnitsEntity entity, int index, IComponent component)
        {
            var collider = entity.unitPhysicCollider.PhysicCollider;
            _colliders.Add(entity, collider);
            _units.Add(collider, entity);
        }
    }
}