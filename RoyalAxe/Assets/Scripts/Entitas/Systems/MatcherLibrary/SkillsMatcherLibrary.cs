using System;
using System.Collections.Generic;
using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public class SkillsMatcherLibrary : BaseMatcherBuilder<SkillEntity>
    {
        public static IMatcherBuilder UsingSkillMather(params IMatcher<SkillEntity>[] triggerDefineMather)
        {
            triggerDefineMather ??= new IMatcher<SkillEntity>[0];
            return new SkillsMatcherLibrary()
            {
                _defineMathchers = new List<IMatcher<SkillEntity>>(triggerDefineMather)
                {
                    SkillMatcher.SkillUse,        // скилл используется 
                    SkillMatcher.UseCounterSkill, // количество использования скила
                    SkillMatcher.PriceUseSkill    // цена использования
                },
                _noneOfMathchers = new List<IMatcher<SkillEntity>>(),
                _anyOfMathchers = new List<IMatcher<SkillEntity>>()
            };
        }
    }
}