using Core.Data.Provider;
using Entitas;
using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;
using RoyalAxe.Units.Player;

namespace RoyalAxe.EntitasSystems
{
    public class DefaultPlayerSkillExecuteSystem : UpdateUsagesSystem, IInitializeSystem
    {
        private UnitsEntity PlayerEntity => _unitsContext.playerEntity;

        private DefaultPlayerSkillSettings _skillSettings;
        private readonly UnitsContext _unitsContext;
        private readonly IDataStorage _dataStorage;
     
        private readonly IBosonUnitPipeline _bosonUnitPipeline;

        private readonly IUnitsEntityFactory _unitsEntityFactory;


        public DefaultPlayerSkillExecuteSystem(IContext<SkillEntity> context,
                                               UnitsContext unitsContext,
                                               IDataStorage dataStorage,
                                               IBosonUnitPipeline bosonUnitPipeline, IUnitsEntityFactory unitsEntityFactory) : base(context)
        {
            _unitsContext  = unitsContext;
            _dataStorage   = dataStorage;
            _bosonUnitPipeline = bosonUnitPipeline;
            _unitsEntityFactory = unitsEntityFactory;
        }

        protected override ICollector<SkillEntity> GetTrigger(IContext<SkillEntity> context)
        {
            return context.CreateCollector(SkillsMatcherLibrary.UsingSkillMather(SkillMatcher.DefaultPlayerSkill, SkillMatcher.MovingToPoint).Added());
        }

        public void Initialize()
        {
            _skillSettings = _dataStorage.ById<SkillsSettings>(DefaultPlayerSkillSettings.SKILL_ID) as DefaultPlayerSkillSettings;
        }

        protected override void DoSkillAction(SkillEntity skill)
        {
            var playerView = PlayerEntity.unitsView.View as PlayerUnitView;
            var spawnPosition = playerView.SkillSpawnTransform;
            

            var bosonEntity = _unitsEntityFactory.CreatePlayerBoson(PlayerEntity);
            _bosonUnitPipeline.CreateBosonInWorld(skill, bosonEntity, _skillSettings, spawnPosition);

            skill.isSkillUse = false;
        }
    }
}