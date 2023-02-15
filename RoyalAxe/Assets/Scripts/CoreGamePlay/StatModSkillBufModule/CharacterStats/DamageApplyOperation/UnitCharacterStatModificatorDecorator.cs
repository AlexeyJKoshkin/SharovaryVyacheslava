namespace RoyalAxe.Units.Stats
{
    public class UnitCharacterStatModificatorDecorator : ICharacterStatModificator
    {
        public IGameStat Stat => _modificator.Stat;
        public CharacterStatValue ModValue => _modificator.ModValue;
        private readonly ICharacterStatModificator _modificator;
        private readonly int _componentId;
        private readonly UnitsEntity _owner;

        public UnitCharacterStatModificatorDecorator(ICharacterStatModificator modificator,
                                                     int componentId,
                                                     UnitsEntity owner)
        {
            _owner       = owner;
            _modificator = modificator;
            _componentId = componentId;
        }

        public ICharacterStatModificator ApplyMod()
        {
            _modificator.ApplyMod();
            ReplaceStat();

            return this;
        }

        public ICharacterStatModificator ApplyPermanentMod()
        {
            _modificator.ApplyPermanentMod();
            ReplaceStat();
            return this;
        }

        public bool RemoveMode()
        {
            if (_modificator.RemoveMode())
            {
                ReplaceStat();
                return true;
            }

            return false;
        }

        private void ReplaceStat()
        {
            var statComponent = _owner.GetComponent(_componentId);
            _owner.ReplaceComponent(_componentId, statComponent);
        }
    }
}