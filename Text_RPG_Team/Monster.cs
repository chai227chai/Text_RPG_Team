using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class Monster : ICharacter
    {
        string name;

        int level;
        int health;
        int attack;
        int drop_exp;

        bool isdead;

        public Monster(string name, int level, int health, int attack) 
        {
            this.name = name;
            this.level = level;
            this.health = health;
            this.attack = attack;
            this.drop_exp = this.level * 10;
            isdead = false;
        }

        public Monster(Monster dupplicate)
        {
            this.name=dupplicate.name;
            this.level = dupplicate.level;
            this.health = dupplicate.health;
            this.attack = dupplicate.attack;
            this.drop_exp = dupplicate.drop_exp;
            isdead = dupplicate.isdead;
        }

        //----------------------------------------------------------------------------------------------
        //변수 반환 함수
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public bool IsDead
        {
            get { return isdead; }
            set { isdead = false; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isdead = true;
            }
        }
    }
}
