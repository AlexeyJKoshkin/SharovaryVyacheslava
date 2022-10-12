using System;
using System.Collections.Generic;
using Core;
using Entitas;
using GameKit;
using UnityEngine;

namespace RoyalAxe.Units
{
    /// <summary>
    ///     Триггер взаимодействия с другими объектами в мире
    ///     настраиваем в инcпекторе доступные тэги взаимодействия.
    ///     при срабатывании триггера заполняем список взаимодействия коллайдеров
    /// </summary>
    public class MeleeTriggerHandler : MonoBehaviour, IViewEntityBehaviour
    {
        [field: SerializeField] public Collider2D PhysicTriggerCollider { get; private set; }
        [SerializeField] private RoyalAxeTagNames _possibleTagInteraction;
        //текущая сущность во вьюшке
        private UnitsEntity _owner;

        public void InitEntity(IEntity entity)
        {
            if (entity is UnitsEntity unitsEntity)
            {
                _owner = unitsEntity;
                _owner.AddEnterPhysicInteraction(new HashSet<Collider2D>());
                _owner.AddUnitPhysicCollider(PhysicTriggerCollider);
            }

            PhysicTriggerCollider.enabled = _owner != null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_owner == null) return;
            if (collision.CompareTag("GameController")) return;

            var checkTagResult = CheckTag(collision.tag);
            if (!checkTagResult.allow)
                return;

            var collection = _owner.enterPhysicInteraction.Add(collision);
            _owner.ReplaceEnterPhysicInteraction(collection);
        }

        private (RoyalAxeTagNames tag, bool allow) CheckTag(string colliderTag)
        {
            RoyalAxeTagNames tagType = RoyalAxeTagNames.None;
            if (!string.IsNullOrEmpty(colliderTag))
            {
                //todo: тут надо чото придумать //мб просто стринги проверять/смотреть
                tagType = (RoyalAxeTagNames) Enum.Parse(typeof(RoyalAxeTagNames), colliderTag);
                return (tagType, _possibleTagInteraction.HasFlag(tagType));
            }

            return (tagType, true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("GameController"))
            {
                _owner.isDestroyUnit = true;
                _owner.isDeadUnit = true;
            }
        }
    }
}