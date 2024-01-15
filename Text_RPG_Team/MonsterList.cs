using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    [Serializable]
    internal class MonsterList
    {
        private List<Monster> monsterList;
        private List<MonsterSkill> skillList;

        int dungeon_stage;

        public MonsterList(int stage)
        {
            dungeon_stage = stage;
            monsterList = new List<Monster>();
            skillList = new List<MonsterSkill>();
            addSkill();
            Initialize();
        }

        //몬스터 추가하기
        public void AddMonster(string name, int level, int health, int attack, int defence, int speed, MonsterType type)
        {
            Monster monster = new Monster(name, level, health, attack, defence, speed, type);
            monsterList.Add(monster);
        }

        //여기에 몬스터를 추가하시면 됩니다.
        public void Initialize()
        {
            if (dungeon_stage >= 1 && dungeon_stage < 4)
            {
                AddMonster("전사 미니언", 2, 15, 5, 0, 1, MonsterType.NIMION);
                AddMonster("마법사 미니언", 3, 10, 6, 0, 1, MonsterType.NIMION);
                AddMonster("공허충", 3, 6, 9, 0, 2, MonsterType.NIMION);
                AddMonster("바위게", 2, 20, 0, 0, 5, MonsterType.NIMION);
            }
            if (dungeon_stage > 2 && dungeon_stage < 7)
            {
                AddMonster("공성 미니언", 5, 25, 8, 1, 1, MonsterType.ELITE_MINION);
            }
            if(dungeon_stage >= 5 && dungeon_stage <= 9 || dungeon_stage > 10)
            {
                AddMonster("늑대", 7, 25, 12, 1, 3, MonsterType.ELITE_WOLF);
                AddMonster("칼날부리", 7, 20, 15, 1, 4, MonsterType.ELITE_BIRD);
            }
            if(dungeon_stage >= 7 && dungeon_stage <= 9 || dungeon_stage > 10)
            {
                AddMonster("독두꺼비", 10, 35, 12, 3, 2, MonsterType.ELITE_FROG);
            }
            if(dungeon_stage == 10 || dungeon_stage > 15)
            {
                AddMonster("공허의 전령", 20, 40, 15, 5, 2, MonsterType.BOSS_HERAID);
            }
        }

        public Monster getMonster(int n)
        {
            return this.monsterList[n - 1];
        }

        //리스트 참조
        public List<Monster> getMonsterList
        {
            get { return monsterList; }
        }

        public List<MonsterSkill> getSkillList
        {
            get { return skillList; }
        }

        void addSkill()
        {
            skillList.Add(new MonsterSkill("대포 발사!!", 1.5f, MonsterType.ELITE_MINION));
            skillList.Add(new MonsterSkill("물어뜯기", 1.5f, MonsterType.ELITE_WOLF));
            skillList.Add(new MonsterSkill("울부짖기", 1.2f, MonsterType.ELITE_WOLF));
            skillList.Add(new MonsterSkill("쪼기", 1.3f, MonsterType.ELITE_BIRD));
            skillList.Add(new MonsterSkill("회전드릴", 2f, MonsterType.ELITE_BIRD));
            skillList.Add(new MonsterSkill("독 내뱉기", 2f, MonsterType.ELITE_FROG));
            skillList.Add(new MonsterSkill("누르기", 1.3f, MonsterType.ELITE_FROG));
            skillList.Add(new MonsterSkill("돌진", 2f, MonsterType.BOSS_HERAID));
        }
    }
}
