using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class MonsterList
    {
        private List<Monster> monsterList = new List<Monster>();

        //몬스터 추가하기
        public void AddMonster(string name, int level, int health, int attack, int defence)
        {
            Monster monster = new Monster(name, level, health, attack, defence);
            monsterList.Add(monster);
        }

        //여기에 몬스터를 추가하시면 됩니다.
        public void Initialize()
        {

        }

        //리스트 참조
        public List<Monster> getMonsterList
        {
            get { return monsterList; }
        }
    }
}
