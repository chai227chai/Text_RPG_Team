using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class MonsterList
    {
        private List<Monster> monsterList;

        int dungeon_stage;

        public MonsterList(int stage)
        {
            dungeon_stage = stage;
            monsterList = new List<Monster>();
            Initialize();
        }

        //몬스터 추가하기
        public void AddMonster(string name, int level, int health, int attack)
        {
            Monster monster = new Monster(name, level, health, attack);
            monsterList.Add(monster);
        }

        //여기에 몬스터를 추가하시면 됩니다.
        public void Initialize()
        {
            AddMonster("미니언", 2, 15, 5);
            AddMonster("마법사 미니언", 3, 10, 6);
            AddMonster("공허충", 3, 6, 9);
            AddMonster("바위게", 2, 20, 0);
            if (dungeon_stage > 2)
            {
                AddMonster("공성 미니언", 5, 25, 8);
            }
            if(dungeon_stage > 4)
            {
                AddMonster("늑대", 7, 25, 12);
                AddMonster("칼날부리", 7, 20, 15);
            }
            if(dungeon_stage > 5)
            {
                AddMonster("독두꺼비", 10, 35, 12);
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
    }
}
