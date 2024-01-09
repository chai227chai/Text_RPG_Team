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

        public void addMonster(string name, int level, int health, int attack)
        {
            Monster monster = new Monster(name, level, health, attack);
            monsterList.Add(monster);
        }


        //리스트 참조
        public List<Monster> getMonsterList
        {
            get { return monsterList; }
        }
    }
}
