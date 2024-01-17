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
        int plus_attack;
        int defence;
        int plus_defence;
        int health;
        int max_health;
        int mp;
        int max_mp;
        int gold;
        int speed;
        int plus_speed;
        int level_exp;
        int[] level_up = new int[2];
        int crit_rate;
        int plus_crit_rate;
        float crit_dmg;
        float plus_crit_dmg;
        int evasion;
        int plus_evasion;

        CHAR_TAG tag;

        bool is_dead;

        public Player(string name, JOB job, int max_health, int max_mp, int attack, int defence, int speed)
        {
            this.name = name;
            this.job = job;
            level = 1;
            this.attack = attack;
            plus_attack = 0;
            this.defence = defence;
            plus_defence = 0;
            this.max_health = max_health;
            health = max_health;
            this.max_mp = max_mp;
            mp = max_mp;
            this.speed = speed;
            plus_speed = 0;
            crit_rate = 15;
            plus_crit_rate = 0;
            crit_dmg = 1.6f;
            plus_crit_dmg = 0f;
            evasion = 10;
            plus_evasion = 0;
            level_exp = 0;
            level_up[0] = 10;
            level_up[1] = 25;
            tag = CHAR_TAG.PLAYER;
            is_dead = false;

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
            get { return max_health; }
            set { max_health = value; }
        }

        //캐릭터 마나
        public int Mp
        {
            get { return mp; }
            set { mp = value; }
        }

        public int MaxMp
        {
            get { return max_mp; }
            set { max_mp = value; }
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
            get { return plus_attack; }
            set { plus_attack = value; }
        }

        //캐릭터 총 공격력
        public int TotalAttack
        {
            get 
            {
                int total_attack = Attack + PlusAttack;
                if(total_attack < 0)
                {
                    total_attack = 0;
                }
                return total_attack;
            }
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
            get { return plus_defence; }
            set { plus_defence = value; }
        }

        //캐릭터 총 방어력
        public int TotalDefence
        {
            get 
            {
                int total_defend = Defence + PlusDefence;
                if(total_defend < 0)
                {
                    total_defend = 0;
                }
                return total_defend;
            }
        }

        //보유 골드
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        //캐릭터 치명타율
        public int CritRate
        {
            get { return crit_rate; }
        }

        //캐릭터 추가 치명타율
        public int PlusCritRate
        {
            get { return plus_crit_rate; }
            set { plus_crit_rate = value; }
        }

        //캐릭터 총 치명타율
        public int TotalCritRate
        {
            get { return CritRate + PlusCritRate; }
        }

        //캐릭터 치명타 데미지
        public float CritDMG
        {
            get { return crit_dmg; }
        }

        //캐릭터 추가 치명타 데미지
        public float PlusCritDMG
        {
            get { return plus_crit_dmg; }
            set { plus_crit_dmg = value; } 
        }

        //캐릭터 총 치명타 데미지
        public float TotalCritDMG
        {
            get { return CritDMG + PlusCritDMG; }
        }

        //캐릭터 회피율
        public int Evasion
        {
            get { return evasion; }
        }

        //캐릭터 추가 회피율
        public int PlusEvasion
        {
            get { return plus_evasion; }
            set { plus_evasion = value; }
        }

        //캐릭터 총 회피율
        public int TotalEvasion
        {
            get { return Evasion + PlusEvasion; }
        }

        //캐릭터 사망 여부
        public bool IsDead
        {
            get { return is_dead; }
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

        //캐릭터 추가 스피드
        public int PlusSpeed
        {
            get { return plus_speed; }
            set { plus_speed = value; }
        }

        //캐릭터 총 스피드
        public int TotalSpeed
        {
            get { return speed + plus_speed; }
        }

        //스킬 리스트
        public List<Skill> getSkillList
        {
            get { return skillList; }
        }

        //인벤토리
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
                is_dead = true;
            }
        }

        //레벨 업
        public void LevelUp(int exp)
        {
            int pre_level = level;
            int pre_attack = Attack;
            int pre_defence = Defence;
            level_exp += exp;
            if(level_exp >= level_up[0])
            {
                level += 1;
                level_exp = 0;
                int sum = level_up[0] + level_up[1];
                level_up[0] = level_up[1];
                level_up[1] = sum;

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
        public int RanSpeed()
        {
            int span = (int)Math.Ceiling((float)TotalSpeed * 0.2f);
            int ran_speed = new Random().Next(TotalSpeed - span, TotalSpeed + span + 1);
            return ran_speed;
        }

        //실 적용 공격력 (공격력 오차값 10% 소수값 올림)
        public int RanAttack()
        {
            int damage_range = (int)Math.Ceiling((float)TotalAttack * 0.1);
            int damage = new Random().Next(TotalAttack - damage_range, TotalAttack + damage_range + 1);
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
