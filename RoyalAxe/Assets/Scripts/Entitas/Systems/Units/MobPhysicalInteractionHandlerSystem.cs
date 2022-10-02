using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Entitas;

namespace RoyalAxe.EntitasSystems
{
    /// <summary>
    ///     Обработчик взаимодействия моба с физ объектами
    /// </summary>
    public class TriggerPhysicalInteraction : ReactiveSystem<UnitsEntity>, IGamePlaySceneSystem
    {
        private readonly Action<UnitsEntity> _handler;

        public TriggerPhysicalInteraction(Action<UnitsEntity> handler, ICollector<UnitsEntity> collector) : base(collector)
        {
            _handler = handler;
        }


        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return null;
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }


        protected override void Execute(List<UnitsEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++) _handler(entities[i]);
        }

      
    }
}