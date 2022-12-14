namespace RoyalAxe.LevelBuff
{
    public class IncreaseCriticalChanceBuff : AbstractBuffStrategy<IncreaseCriticalChanceBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;


        public IncreaseCriticalChanceBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoBuffStrategyActivate() { }
    }
}