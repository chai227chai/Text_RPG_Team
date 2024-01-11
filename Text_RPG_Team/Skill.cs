using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    public class Skill
    {
        public string Name { get; }
        public string Description { get; }
        public int MP { get; }
        public int Coefficient { get; }
        public int Range { get; }

        public static int SkillNum;

        public static int SkillCnt = 0;

        public static Skill[] _skills;

        public Skill(string name, string description, int mp, int coefficient, int range)
        {
            Name = name;
            Description = description;
            MP = mp;
            Coefficient = coefficient;
            Range = range;
        }

        public static void AddSkill(Skill skill)
        {
            if (SkillCnt == 10)
            {
                return;
            }

            _skills[SkillCnt] = skill;
            SkillCnt++;
        }
    }
}
