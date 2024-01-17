using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    [Serializable]
    internal class Skill : ISkill
    {
        public string Name { get; }
        public string Description { get; }
        public int MP { get; }
        public float Coefficient { get; }
        public int Range { get; }
        public SkillAbility SkillAbility { get; }

        public Skill(string name, string description, int mp, float coefficient, int range, SkillAbility skillAbility)
        {
            Name = name;
            Description = description;
            MP = mp;
            Coefficient = coefficient;
            Range = range;
            SkillAbility = skillAbility;
        }

    }
}
