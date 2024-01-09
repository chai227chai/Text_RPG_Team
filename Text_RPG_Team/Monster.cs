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
        int drop_exp;// 몬스터가 제공하는 경험치

        bool isdead;

        public Monster(string name, int level, int health, int attack) 
        {
            this.name = name;
            this.level = level;
            this.health = health;
            this.attack = attack;
            this.drop_exp = this.level * 10;//경험치 = 몬스터의 레벨 * 10
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

        //몬스터 이름
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //몬스터 레벨
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        //몬스터 체력
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        //몬스터 공격력
        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        //몬스터 사망 여부
        public bool IsDead
        {
            get { return isdead; }
            set { isdead = false; }
        }

        //몬스터가 제공하는 경험치
        public int Drop_Exp
        {
            get { return drop_exp; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수

        //몬스터 피격 시
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
