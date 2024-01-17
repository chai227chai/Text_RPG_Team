using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Text_RPG_Team
{
    [Serializable]
    internal class MonsterSkill : ISkill
    {
        public string Name { get; }
        public float Coefficient { get; }
        public MonsterType Type { get; }
        public SkillAbility SkillAbility { get; }

        public MonsterSkill(string name, float coefficient, MonsterType type, SkillAbility skillAbility)
        {
            Name = name;
            Coefficient = coefficient;
            Type = type;
            SkillAbility = skillAbility;
        }
    }
}
