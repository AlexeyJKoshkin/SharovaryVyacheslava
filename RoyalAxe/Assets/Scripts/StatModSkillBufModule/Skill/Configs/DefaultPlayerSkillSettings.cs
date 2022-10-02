using UnityEngine;

namespace RoyalAxe.CharacterStat
{
    [CreateAssetMenu]
    public class DefaultPlayerSkillSettings : SkillsSettings
    {
        public const string SKILL_ID = "DefaultPlayerSkill";
        public override string UniqueID => SKILL_ID;
    }
}