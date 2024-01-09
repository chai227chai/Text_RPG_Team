using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    public enum JOB
    {
        WARRIOR, WIZARD, ROGUE
    }

    internal class Player : ICharacter
    {
        string name;
        JOB job;

        int level;
        int attack;
        int defence;
        int health;
        int gold;

        bool isdead;

        public Player()
        {
            name = "Chad";
            job = JOB.WARRIOR;
            level = 1;
            attack = 10;
            defence = 5;
            health = 100;
            gold = 1500;

            isdead = false;
        }

        //----------------------------------------------------------------------------------------------
        //변수 반환 함수
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Job
        {
            get 
            { 
                switch (this.job)
                {
                    case JOB.WARRIOR:
                        return "전사";
                    case JOB.WIZARD:
                        return "마법사";
                    case JOB.ROGUE:
                        return "도적";
                    default:
                        return "초보자";
                } 
            }
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

        public int Defense
        {
            get { return defence; }
            set { defence = value; }
        }

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public bool IsDead
        {
            get { return isdead; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수
        public void TakeDamage(int damage)
        {
            health = health - (damage - defence);
            if (health <= 0)
            {
                isdead = true;
            }
        }

        //----------------------------------------------------------------------------------------------
        //상태보기
        public void ViewState()
        {
            Console.Clear();
            Console.WriteLine("■상태보기■");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine("");
            Console.WriteLine("LV : " + level.ToString("00"));
            Console.WriteLine($"{name} ( {Job} )");
            Console.WriteLine($"공격력 : {attack}");
            Console.WriteLine($"방어력 : {defence}");
            Console.WriteLine($"체  력 : {health}");
            Console.WriteLine($"Gold : {Gold}G");
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
        }
    }
}
