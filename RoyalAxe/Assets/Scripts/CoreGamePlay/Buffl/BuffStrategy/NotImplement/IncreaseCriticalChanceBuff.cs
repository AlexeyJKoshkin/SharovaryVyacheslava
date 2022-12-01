namespace RoyalAxe.LevelBuff
{
    public class IncreaseCriticalChancePower : AbstractPowerStrategyStrategy<IncreaseCriticalChanceBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;


        public IncreaseCriticalChancePower(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate() { }
        public override void DoLevelPowerDeActivate()
        {
            
        }
    }
}