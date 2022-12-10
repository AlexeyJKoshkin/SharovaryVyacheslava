using RoyalAxe.Units.Stats;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public interface IBosonUnitPipeline
    {
        void CreateBosonInWorld(SkillEntity skill, UnitsEntity bosonEntity, SkillsSettings skillSettings, Transform spawnPosition);
    }
}