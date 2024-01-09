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

        public MonsterList()
        {
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
            AddMonster("대포 미니언", 5, 25, 8);
            AddMonster("공허충", 3, 10, 9);
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
