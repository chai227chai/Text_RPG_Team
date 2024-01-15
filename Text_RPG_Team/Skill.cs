using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    [Serializable]
    public class Skill : ISkill
    {
        public string Name { get; }
        public string Description { get; }
        public int MP { get; }
        public float Coefficient { get; }
        public int Range { get; }

        public static int SkillNum;

        public static int SkillCnt = 0;

        public Skill(string name, string description, int mp, float coefficient, int range)
        {
            Name = name;
            Description = description;
            MP = mp;
            Coefficient = coefficient;
            Range = range;
        }

    }
}
