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

        public static int SkillCnt = 0;

        static Skill[] _skills;

        public Skill(string name, string description, int mp, int coefficient, int range)
        {
            Name = name;
            Description = description;
            MP = mp;
            Coefficient = coefficient;
            Range = range;
        }

        static void AddSkill(Skill skill)
        {
            if (SkillCnt == 10)
            {
                return;
            }

            _skills[SkillCnt] = skill;
            SkillCnt++;
        }

        static void UseSkill()
        {
            AddSkill(new Skill("파워 스트라이크", "한 명의 적에게 강한 데미지를 가합니다.", 5, 3, 1));
            AddSkill(new Skill("슬래시 블러스트", "모든 적에게 데미지를 가합니다.", 10, 2, 2));
        }
    }
}
