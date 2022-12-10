using Entitas;
using RoyalAxe.Units.Stats;
using RoyalAxe.Configs;
using RoyalAxe.GameEntitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class BosonUnitPipeline : IBosonUnitPipeline
    {
        private readonly IUnitsViewBuilder _unitViewBuilder;
        private readonly UnitsContext _unitsContext;
        private readonly RAAnimationContext _animationContext;

        public BosonUnitPipeline(IUnitsViewBuilder unitViewBuilder, UnitsContext unitsContext, RAAnimationContext animationContext)
        {
            _unitViewBuilder = unitViewBuilder;
            _unitsContext    = unitsContext;
            _animationContext = animationContext;
        }

        public void CreateBosonInWorld(SkillEntity skill, UnitsEntity bosonEntity, SkillsSettings skillSettings, Transform spawnTransform)
        {
            var     config        = skillSettings.BosonConfig;
            Vector2 spawnPosition = spawnTransform.position;

            if (skill.isDoubleAxe)
            {
                var newSpawnPos = spawnTransform.TransformPoint(new Vector3(0, -skillSettings.Size, 0));

                CreateCopyAxe(bosonEntity, config, newSpawnPos, skill.movingToPoint.PointAdapter);
            }

            if (skill.isTripleAxe)
            {
                var     originVector          = skill.movingToPoint.PointAdapter.TargetPosition - spawnPosition;
                Vector2 leftDestinationPoint  = RotateVector(originVector, -45) - spawnPosition;
                Vector2 rightDestinationPoint = RotateVector(originVector, 45) - spawnPosition;
                CreateCopyAxe(bosonEntity, config, spawnPosition, new SimpleVector2Adapter(leftDestinationPoint));
                CreateCopyAxe(bosonEntity, config, spawnPosition, new SimpleVector2Adapter(rightDestinationPoint));
            }

            CreateView(bosonEntity, config, spawnPosition, skill.movingToPoint.PointAdapter);
            skill.RemoveMovingToPoint();
        }

        private void CreateCopyAxe(UnitsEntity bosonEntity, UnitConfigDef config, Vector2 spawnPosition, IPointAdapter pointAdapter)
        {
            var copy = CreateCopy(bosonEntity);
            CreateView(copy, config, spawnPosition, pointAdapter);
            copy.isAdditionalBoson = true;
        }

        private Vector2 RotateVector(Vector3 originVector, int angel)
        {
            return Quaternion.Euler(0, 0, angel) * originVector * 1.5f; // todo: получать точку из карты
        }

        private UnitsEntity CreateCopy(UnitsEntity bosonEntity)
        {
            var copy = _unitsContext.CreateEntity();
            bosonEntity.CopyTo(copy);
            copy.ReplaceUnitAnimationEntity(_animationContext.CreateEntity());
            return copy;
        }

        private void CreateView(UnitsEntity bosonEntity, UnitConfigDef config, Vector3 spawnPosition, IPointAdapter pointAdapter)
        {
            _unitViewBuilder.BuildBosonView(bosonEntity, config, spawnPosition);
            bosonEntity.ReplaceMovingToPoint(pointAdapter);
        }
    }
}