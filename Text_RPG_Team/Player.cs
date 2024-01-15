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

    [Serializable]
    internal class Player : ICharacter
    {
        Inventory inventory = new Inventory();

        private List<Skill> skillList;

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
        int maxmp;
        int gold;
        int speed;
        int plusSpeed;
        int levelexp;
        int[] levelup = new int[2];

        CHAR_TAG tag;

        bool isdead;

        public Player(string name, JOB job, int maxHealth, int maxMp, int attack, int defence, int speed)
        {
            this.name = name;
            this.job = job;
            level = 1;
            this.attack = attack;
            plusattack = 0;
            checkattack = false;
            this.defence = defence;
            plusdefence = 0;
            checkdefence = false;
            maxhealth = maxHealth;
            health = maxhealth;
            maxmp = maxMp;
            mp = maxmp;
            this.speed = speed;
            plusSpeed = 0;
            levelexp = 0;
            levelup[0] = 10;
            levelup[1] = 25;
            tag = CHAR_TAG.PLAYER;
            isdead = false;

            skillList = new List<Skill>();
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

        public int MaxMp
        {
            get { return maxmp; }
            set { maxmp = value; }
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

        public int Total_Attack
        {
            get { return Attack + PlusAttack; }
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

        //캐릭터 총 방어력
        public int Total_Defence
        {
            get { return Defence + PlusDefence; }
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

        public int PlusSpeed
        {
            get { return plusSpeed; }
            set { plusSpeed = value; }
        }

        public int Total_Speed
        {
            get { return speed + plusSpeed; }
        }

        public List<Skill> getSkillList
        {
            get { return skillList; }
        }

        public Inventory GetInventory
        {
            get { return inventory; }
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
            int pre_level = level;
            int pre_attack = Attack;
            int pre_defence = Defence;
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
                Console.WriteLine($"Lv. {pre_level} -> Lv. {Level}");
                Console.WriteLine($"ATK {pre_attack} -> ATK {Attack}");
                Console.WriteLine($"DEF {pre_defence} -> DEF {Defence}");
                Console.WriteLine($"Lv.{Level} {Name}");
                
            }
        }

        //실 적용 스피드 (스피드 오차값 20% 소수값 올림)
        public int Ran_Speed()
        {
            int span = (int)Math.Ceiling((float)Total_Speed * 0.2f);
            int ran_speed = new Random().Next(Total_Speed - span, Total_Speed + span + 1);
            return ran_speed;
        }

        //실 적용 공격력 (공격력 오차값 10% 소수값 올림)
        public int Ran_Attack()
        {
            int damage_range = (int)Math.Ceiling((float)Total_Attack * 0.1);
            int damage = new Random().Next(Total_Attack - damage_range, Total_Attack + damage_range + 1);
            return damage;
        }


        //---------------------------------------------------------------------------------------------------------------
        //스킬 부여 함수
        public void UseSkill()
        {
            switch (this.job)
            {
                case JOB.WARRIOR:
                    skillList.Add(new Skill("파워 스트라이크", "한 명의 적에게 공격력 * 3 의 강한 데미지를 가합니다.", 5, 3, 1));
                    skillList.Add(new Skill("슬래시 블러스트", "모든 적에게 공격력 * 2 의 데미지를 가합니다.", 10, 2, 0));
                    skillList.Add(new Skill("더블 스트라이크", "무작위 적 2명에게 공격력 * 1.5 의 데미지를 가합니다.", 5, 2, 2));
                    break;
                case JOB.WIZARD:
                    skillList.Add(new Skill("에너지 볼트", "한 명의 적에게 공격력 * 2 의 데미지를 입힙니다.", 20, 2, 1));
                    skillList.Add(new Skill("메테오 스트라이크", "모든 적에게 공격력 * 5 의 강력한 데미지를 입힙니다.", 100, 5, 0));
                    skillList.Add(new Skill("체인 라이트닝", "무작위 적 3명에게 공격력 * 2.5 의 데미지를 입힙니다.", 50, 2.5f, 3));
                    break;
                case JOB.ROGUE:
                    skillList.Add(new Skill("부식", "한 명의 적에게 공격력 * 2 의 데미지를 줍니다.", 10, 2, 1));
                    skillList.Add(new Skill("기습", "무작위 적 2명에게 공격력 * 6 의 강력한 데미지를 줍니다.", 100, 6, 2));
                    skillList.Add(new Skill("암살", "한 명의 적에게 공격력 * 10 의 치명적인 데미지를 줍니다.", 100, 10, 1));
                    break;
            }
        }

    }
}
