using Core.Data.Provider;
using RoyalAxe.Units.Stats;
using RoyalAxe.Configs;

namespace RoyalAxe.GameEntitas
{
    internal class UnitStatsBuilder
    {
        private IDataStorage _dataStorage;
        public UnitStatsBuilder(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            
            //_dataStorage.ById<CharacterStatTypeParameters>()
        }


        #region STATS


        //установили все статы на персонажа
        public void SetStats(UnitsEntity unit, StatsConfig config)
        {
            //_dataStorage.ById<CharacterStatTypeParameters>() // отсюда брать параметры каждого стата
            AddStat(unit, UnitsComponentsLookup.Health, CharacterStatValue.CreateState(config.Health, config.Health));
            AddStat(unit, UnitsComponentsLookup.AttackSpeed, CharacterStatValue.CreateState());
            AddStat(unit, UnitsComponentsLookup.MoveSpeed, CharacterStatValue.CreateState(config.MoveSpeed,100,0));
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