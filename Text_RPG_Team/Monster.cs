using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Text_RPG_Team
{
    public enum MonsterType
    {
        NIMION, ELITE_MINION, ELITE_WOLF, ELITE_BIRD, ELITE_FROG, BOSS_HERAID
    }

    internal class Monster : ICharacter
    {
        private Random random = new Random();

        string name;

        int level;
        int health;
        int attack;
        int defence;
        int speed;
        int drop_exp;// 몬스터가 제공하는 경험치

        CHAR_TAG tag;
        MonsterType type;

        bool isdead;

        public Monster(string name, int level, int health, int attack, int defence, int speed, MonsterType type) 
        {
            this.name = name;
            this.level = level;
            this.health = health;
            this.attack = attack;
            this.speed = speed;
            this.defence = defence;
            this.drop_exp = this.level;//경험치 = 몬스터의 레벨 1당 1의 경험치

            tag = CHAR_TAG.MONSTER;
            this.type = type;

            isdead = false;
        }

        public Monster(Monster dupplicate)
        {
            this.name=dupplicate.name;
            this.level = dupplicate.level;
            this.health = dupplicate.health;
            this.attack = dupplicate.attack;
            this.defence = dupplicate.defence;
            this.speed = dupplicate.speed;
            this.drop_exp = dupplicate.drop_exp;

            tag = dupplicate.tag;
            this.type = dupplicate.type;

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

        public string getHP
        {
            get
            {
                if(health > 0)
                {
                    return health.ToString();
                }
                else
                {
                    return "dead";
                }
            }
        }

        //몬스터 공격력
        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        // 몬스터 방어력
        public int Defence
        {
            get { return defence; }
            set { defence = value; }
        }

        public int Total_Defence
        {
            get { return Defence; }
        }

        //몬스터 스피드 (오차값 20% 소수값 올림)
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
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

        //캐릭터 태그
        public CHAR_TAG Tag
        {
            get { return tag; }
        }

        //몬스터 종류
        public MonsterType Type
        {
            get { return type; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수

        //몬스터 피격 시
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isdead = true;
            }
        }

        //실 적용 스피드 (스피드 오차값 20% 소수값 올림)
        public int Ran_Speed()
        {
            int span = (int)Math.Ceiling((float)speed * 0.2f);
            int ran_speed = random.Next(speed - span, speed + span + 1);
            return ran_speed;
        }

        //실 적용 공격력 (공격력 오차값 10% 소수값 올림)
        public int Ran_Attack()
        {
            int damage_range = (int)Math.Ceiling((float)attack * 0.1);
            int damage = random.Next(attack - damage_range, attack + damage_range + 1);
            return damage;
        }

        public string[] DropTable(Monster monster)
        {
            string[] dropTable = new string[] {};

            switch (monster.Type)
            {
                case MonsterType.NIMION:
                    dropTable = new string[] { "0", "3" };
                    break;
                case MonsterType.ELITE_MINION:
                    dropTable = new string[] { "0", "3", "1" };
                    break;
                case MonsterType.ELITE_WOLF: case MonsterType.ELITE_BIRD:
                    dropTable = new string[] { "0", "3", "1", "4" };
                    break;
                case MonsterType.ELITE_FROG:
                    dropTable = new string[] { "6", "9", "12" };
                    break;
            }

            return dropTable;
        }

        public string dropItem(Monster monster)
        {
            Random ran = new Random();

            switch (monster.Type)
            {
                case MonsterType.NIMION:
                    int drop = ran.Next(0, 11);
                    if (drop <= 1)
                    {
                        return DropTable(monster)[ran.Next(0, 2)];
                    }
                    break;
                case MonsterType.ELITE_MINION:
                    drop = ran.Next(0, 11);
                    if (drop <= 2)
                    {
                        return DropTable(monster)[ran.Next(0, 2)];
                    }
                    else if(drop == 3)
                    {
                        return DropTable(monster)[2];
                    }
                    break;
                case MonsterType.ELITE_WOLF: case MonsterType.ELITE_BIRD:
                    drop = ran.Next(0, 100);
                    if (drop < 30)
                    {
                        return DropTable(monster)[ran.Next(0, 2)];
                    }
                    else if (drop < 40 && drop >= 30)
                    {
                        return DropTable(monster)[2];
                    }
                    else if(drop < 45 && drop >= 40)
                    {
                        return DropTable(monster)[3];
                    }
                    break;
                case MonsterType.ELITE_FROG:
                    drop = ran.Next(0, 100);
                    if (drop <= 0)
                    {
                        return DropTable(monster)[ran.Next(1, 3)];
                    }
                    else if (drop <= 10 && drop > 0)
                    {
                        return DropTable(monster)[0];
                    }
                    break;
            }

            return "-1";
        }

    }
}
