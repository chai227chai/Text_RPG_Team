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
        float attack;
        int defence;
        int health;
        int maxhealth;
        int gold;
        int speed;
        int levelexp;
        int[] levelup = new int[2];

        bool isdead;

        public Player()
        {
            name = "Chad";
            job = Job;
            level = 1;
            attack = Attack;
            defence = Defence;
            health = Health;
            gold = 1500;
            levelexp = 0;
            levelup[0] = 10;
            levelup[1] = 25;    
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

        //캐릭터 최대 체력
        public int MaxHealth
        {
            get { return maxhealth; }
            set { maxhealth = value; }
        }

        //캐릭터 공격력
        public float Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        //캐릭터 방어력
        public int Defence
        {
            get { return defence; }
            set { defence = value; }
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
        //캐릭터 스피드
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수

        //피격 받음
        public void TakeDamage(float damage)
        {
            health = health - (int)(damage - defence);
            if (health <= 0)
            {
                health = 0;
                isdead = true;
            }
        }

        //레벨 업
        public void LevelUp(int exp)
        {
            levelexp += exp;
            if(levelexp >= levelup[0])
            {
                level += 1;
                attack += 0.5f;
                defence += 1;
                levelexp = 0;
                int sum = levelup[0] + levelup[1];
                levelup[0] = levelup[1];
                levelup[1] = sum;
            }
        }
    }
}
