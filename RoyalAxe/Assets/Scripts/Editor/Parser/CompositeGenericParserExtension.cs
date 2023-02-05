using RoyalAxe.Units.Stats;

namespace Core.Parser {
    public static class CompositeGenericParserExtension
    {
        public static CompositeGenericParser BindWeaponSkills(this CompositeGenericParser parser)
        {
            return parser.Bind<SkillConfigDef.RangeParams>()
                  .Bind<SkillConfigDef.Damage>()
                  .Bind<SingleDamageInfo>();
        }
    }
}