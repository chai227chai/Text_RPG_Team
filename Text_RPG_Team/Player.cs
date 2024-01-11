using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private Random random = new Random();

        string name;
        JOB job;

        int level;
        int attack;
        int plusattack;
        bool checkattack;
        int defence;
        int plusdefence;
        bool checkdefence;
        int health;
        int maxhealth;
        int mp;
        int gold;
        int speed;
        int levelexp;
        int[] levelup = new int[2];

        CHAR_TAG tag;

        bool isdead;

        public Player()
        {
            name = "Chad";
            job = Job;
            level = 1;
            attack = Attack;
            plusattack = 0;
            checkattack = false;
            defence = Defence;
            plusdefence = 0;
            checkdefence = false;
            health = Health;
            maxhealth = MaxHealth;
            mp = Mp;
            speed = Speed;
            gold = Gold;
            levelexp = 0;
            levelup[0] = 10;
            levelup[1] = 25;
            tag = CHAR_TAG.PLAYER;
            isdead = false;
        }

        //----------------------------------------------------------------------------------------------
        //변수 반환 함수

        //캐릭터 이름
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //캐릭터 직업
        public JOB Job
        {
            get { return this.job; }
            set {  this.job = value; }
        }

        //캐릭터 직업 출력
        public string GetJob
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

        //캐릭터 레벨
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        //캐릭터 체력
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int MaxHealth
        {
            get { return maxhealth; }
            set { maxhealth = value; }
        }

        //캐릭터 마나
        public int Mp
        {
            get { return mp; }
            set { mp = value; }
        }

        //캐릭터 공격력
        public int Attack
        {
            get
            {
                int now_attack = this.attack + ((level - 1) / 2);
                return now_attack; 
            }
            set { attack = value; }
        }

        //캐릭터 추가 공격력
        public int PlusAttack
        {
            get { return plusattack; }
            set { plusattack = value; }
        }

        public bool CheckAttack
        {
            get { return checkattack; }
            set { checkattack = value; }
        }

        //캐릭터 방어력
        public int Defence
        {
            get 
            {
                int now_defence = this.defence + (level - 1);
                return now_defence; 
            }
            set { defence = value; }
        }

        //캐릭터 추가 방어력
        public int PlusDefence
        {
            get { return plusdefence; }
            set { plusdefence = value; }
        }

        public bool CheckDefence
        {
            get { return checkdefence; }
            set { checkdefence = value; }
        }

        //보유 골드
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        //캐릭터 사망 여부
        public bool IsDead
        {
            get { return isdead; }
        }

        //캐릭터 태그
        public CHAR_TAG Tag
        {
            get { return tag; }
        }

        //캐릭터 스피드
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수

        //피격 받음
        public void TakeDamage(int damage)
        {
            health = health - (damage);
            if (health <= 0)
            {
                health = 0;
                isdead = true;
            }
        }

        //레벨 업
        public void LevelUp(int exp)
        {
            int prev_level = level;
            levelexp += exp;
            if(levelexp >= levelup[0])
            {
                level += 1;
                levelexp = 0;
                int sum = levelup[0] + levelup[1];
                levelup[0] = levelup[1];
                levelup[1] = sum;

                Console.WriteLine();
                Console.WriteLine("Level UP");
                Console.WriteLine();
                Console.WriteLine($"Lv. {prev_level} -> Lv. {Level}");
                Console.WriteLine($"Lv.{Level} {Name}");
                
            }
        }

        //실 적용 스피드 (스피드 오차값 20% 소수값 올림)
        public int SetSpeed()
        {
            int span = (int)Math.Ceiling((float)speed * 0.2f);
            int ran_speed = random.Next(speed - span, speed + span + 1);
            return ran_speed;
        }

        public int Damage_check(int attack)
        {
            int damage_range = (int)Math.Ceiling((float)attack * 0.1);
            int damage = random.Next(attack - damage_range, attack + damage_range + 1);
            return damage;
        }
    }
}
