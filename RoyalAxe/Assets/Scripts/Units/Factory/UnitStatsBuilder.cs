using Core.Data.Provider;
using RoyalAxe.CharacterStat;
using RoyalAxe.Configs;

namespace RoyalAxe.GameEntitas
{
    internal class UnitStatsBuilder
    {
        private readonly IDataStorage _dataStorage;

        public UnitStatsBuilder(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        #region STATS

        public UnitsEntity AddStats(UnitsEntity unit, string unitId, byte level = 1)
        {
            var statsConfig = _dataStorage.ById<StatCollection>(unitId).Stats[level];
            SetStats(unit, statsConfig);
            return unit;
        }

        //установили все статы на персонажа
        private void SetStats(UnitsEntity unit, StatsConfig config)
        {
            AddStat(unit, UnitsComponentsLookup.Health, CharacterStatValue.CreateState(config.Health, config.Health));
            AddStat(unit, UnitsComponentsLookup.AttackSpeed, CharacterStatValue.CreateState());
            AddStat(unit, UnitsComponentsLookup.MoveSpeed, CharacterStatValue.CreateState(config.MoveSpeed));
            AddStat(unit, UnitsComponentsLookup.PhysicalDamageStat, CharacterStatValue.CreateState());
            AddStat(unit, UnitsComponentsLookup.FireDamageStat, CharacterStatValue.CreateState());
            AddStat(unit, UnitsComponentsLookup.ColdDamageStat, CharacterStatValue.CreateState());
            AddStat(unit, UnitsComponentsLookup.PoisonDamageStat, CharacterStatValue.CreateState());
        }

        private void AddStat(UnitsEntity entity, int idComponent, CharacterStatValue statValue)
        {
            if (entity == null)
            {
                return;
            }

            var component = entity.CreateComponent(idComponent, UnitsComponentsLookup.componentTypes[idComponent]) as ModifiableStat;
            component.UnitStatValue = statValue;
            entity.AddComponent(idComponent, component);
        }

        #endregion

        public void CopyStats(UnitsEntity recipient, UnitsEntity donor)
        {
            CopyStat(recipient, UnitsComponentsLookup.Health, donor);
            CopyStat(recipient, UnitsComponentsLookup.AttackSpeed, donor);
            CopyStat(recipient, UnitsComponentsLookup.MoveSpeed, donor);
            CopyStat(recipient, UnitsComponentsLookup.PhysicalDamageStat, donor);
            CopyStat(recipient, UnitsComponentsLookup.FireDamageStat, donor);
            CopyStat(recipient, UnitsComponentsLookup.PoisonDamageStat, donor);
            CopyStat(recipient, UnitsComponentsLookup.ColdDamageStat, donor);
        }

        private void CopyStat(UnitsEntity recipient, int idComponent, UnitsEntity donor)
        {
            CharacterStatValue statValue = CharacterStatValue.Default000;
            if (donor.HasComponent(idComponent))
            {
                var unitStat = donor.GetComponent(idComponent) as ModifiableStat;
                statValue = unitStat.NativeStatValue;
            }

            AddStat(recipient, idComponent, statValue);
        }
    }
}