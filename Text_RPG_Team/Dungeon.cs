using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class Dungeon
    {
        private Random random = new Random();

        Player? player;
        MonsterList? monsterList;
        Portion HPportion = new Portion(PortionType.HP);
        Portion MPportion = new Portion(PortionType.MP);
        List<Monster>? battle_monster;
        ICharacter[]? allCharacter;

        int player_health;
        int player_mp;
        int stage_exp;
        int stage;


        public Dungeon()
        {
            stage = 1;
        }

        public int Now_Stage
        {
            get { return stage; }
        }

        //던전 생성
        public void GoDungeon(Player player)
        {
            this.player = player;
            this.monsterList = new MonsterList(stage);

            this.battle_monster = new List<Monster>();

            player_health = player.Health;
            player_mp = player.Mp;

            int number = 1;
            if(stage >= 1 && stage <= 4)
            {
                number = random.Next(stage, stage + 1);
            }
            else if(stage >= 5 && stage <= 9)
            {
                number = random.Next(3, 6);
            }
            else if(stage == 10)
            {
                number = 1;
            }
            else if(stage > 10)
            {
                number = random.Next(stage, stage + 1);
            }

            int exp = 0;

            allCharacter = new ICharacter[number + 1];
            allCharacter[0] = player;

            for (int i = 0; i < number; i++)
            {
                int random_monster = random.Next(1, monsterList.getMonsterList.Count+1);
                Monster newMonster = new Monster(monsterList.getMonster(random_monster));
                battle_monster.Add(newMonster);

                allCharacter[i+1] = newMonster;
                exp += newMonster.Drop_Exp;
            }
            stage_exp = exp;

            CheckName(battle_monster);

            Start_phase();
        }

        //---------------------------------------------------------------------------------------------------------------
        private void Start_phase()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Console.WriteLine($"{stage}층에 입장합니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            
            Console.WriteLine("몬스터들이 당신의 앞을 막아섭니다.");
            Console.WriteLine();
            foreach (Monster mon in battle_monster)
            {
                Console.WriteLine($"Lv.{mon.Level} {mon.Name} ");
            }
            Console.WriteLine();
            Console.WriteLine(">> 다음");
            Console.ReadKey();

            Battle_phase();
        }

        //---------------------------------------------------------------------------------------------------------------
        //전투 페이즈
        private void Battle_phase()
        {
            int death_cnt = 0;
            int turn = 0;

            while (!player.IsDead && death_cnt < battle_monster.Count)
            {
                //죽은 몬스터 수
                death_cnt = 0;

                //턴 수 증가
                turn++;

                //턴 시작 시 마다 스피드 별로 정렬
                allCharacter = allCharacter.OrderByDescending(x => x.Ran_Speed()).ToArray();

                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                Console.WriteLine($"{turn} 번째 턴");
                Console.WriteLine();
                Thread.Sleep(500);

                //모든 캐릭 한번 씩 돌아가며 행동
                foreach (ICharacter character in allCharacter)
                {
                    //캐릭이 플레이어일 때
                    if(character.Tag == CHAR_TAG.PLAYER && !character.IsDead)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("당신의 차례입니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);

                        PlayerTurn();
                    }
                    else if(character.Tag == CHAR_TAG.PLAYER && character.IsDead)
                    {
                        break;
                    }
                    //캐릭이 몬스터일 때
                    else if(character.Tag == CHAR_TAG.MONSTER && !character.IsDead)
                    {
                        EnemyTurn((Monster)character);
                    }
                }

                //죽은 몬스터 수 체크해서 던전 클리어 확인
                foreach(Monster mon in battle_monster)
                {
                    if (mon.IsDead)
                    {
                        death_cnt++;
                    }
                }

            }

            Result();
        }

        //---------------------------------------------------------------------------------------------------------------
        //던전 탐험 결과
        private void Result()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();

            if (player.IsDead)
            {
                Console.WriteLine("You Lose");
                Console.WriteLine();

                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player_health} -> {player.Health}");

                Console.WriteLine();
                Console.WriteLine("당신은 던전에서 도망쳤습니다.");
                stage = 1;

                Console.WriteLine();
                Console.WriteLine("0. 다음");

                int act = IsValidInput(0, 0);

                if (act == 0)
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("Victory");
                Console.WriteLine();

                Console.WriteLine($"던전에서 몬스터 {battle_monster.Count}마리를 잡았습니다.");
                Console.WriteLine();

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player_health} -> {player.Health}");

                int currentMp = player.Mp;
                player.Mp = player.Mp + 10 > player_mp ? player_mp : player.Mp + 10;
                Console.WriteLine($"MP {player_mp} -> {currentMp}");
                Console.WriteLine($"MP 회복(10) -> {player.Mp}");

                Console.WriteLine();
                Rewards();

                Console.WriteLine();
                player.LevelUp(stage_exp);

                Console.WriteLine();
                Console.WriteLine("다음 층으로 올라갑니다.");
                stage++;

                Console.WriteLine();
                Console.WriteLine("0. 다음");

                int act = IsValidInput(0, 0);

                if (act == 0)
                {
                    return;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        //몬스터 보상계산
        private void Rewards()
        {
            int rewardGold = 0;
            int rewardHpPotion = 0;
            int rewardMpPotion = 0;
            int i = random.Next(1, 5);
            foreach(Monster mon in battle_monster)
            {
                rewardGold = 300 * mon.Level;
                if(i <= 2)
                {
                    rewardHpPotion += 1;
                }
                else if (i <= 3)
                {
                    rewardMpPotion += 1;
                }
            }
            player.Gold += rewardGold;
            HPportion.SetPortion(rewardHpPotion);
            MPportion.SetPortion(rewardMpPotion);
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{rewardGold} Gold");
            if(rewardHpPotion >= 1)
            {
                Console.WriteLine($"체력 회복 포션 - {rewardHpPotion}");
            }
            if(rewardMpPotion >= 1)
            {
                Console.WriteLine($"마나 회복 포션 - {rewardMpPotion}");
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        //플레이어 행동 턴
        private void PlayerTurn()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            int index = 1;
            foreach (Monster mon in battle_monster)
            {
                if (mon.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.WriteLine($"Lv.{mon.Level} {mon.Name}   HP {mon.getHP}  ATK {mon.Attack}");
                Console.ResetColor();
                index++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}  ({player.GetJob})");
            Console.WriteLine();
            Console.WriteLine($"HP {player.Health} / {player.MaxHealth}");
            Console.WriteLine($"MP {player.Mp} / {player.MaxMp}");

            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            int act = IsValidInput(2, 1);

            int target;
            if(act == 1)
            {
                //공격 행동 선택 시 타겟을 선택
                target = SelectTarget();

                //아무것도 선택하지 않았을 때
                if(target == 0)
                {
                    //다시 되돌아 옴
                    PlayerTurn();
                    return;
                }
                else
                {
                    //대상을 선택했다면, 공격
                    Console.Clear();
                    Attack(player, battle_monster[target - 1]);
                }
                Console.WriteLine();
                Console.WriteLine(">> 다음");
                Console.ReadKey();

            }
            else if (act == 2)
            {
                PlayerSkillTurn();
            }
        }
        //---------------------------------------------------------------------------------------------------
        //스킬 선택 시 턴
        private void PlayerSkillTurn()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            int index = 1;
            foreach (Monster mon in battle_monster)
            {
                if (mon.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.WriteLine($"Lv.{mon.Level} {mon.Name}   HP {mon.getHP}");
                Console.ResetColor();
                index++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}  ({player.GetJob})");
            Console.WriteLine($"HP {player.Health}");
            Console.WriteLine($"MP {player.Mp}");
            Console.WriteLine();

            index = 1;
            foreach(Skill skill in player.getSkillList)
            {
                Console.Write(index);
                Console.WriteLine(". " + skill.Name + " - MP " + skill.MP);
                Console.WriteLine("   " + skill.Description);
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");

            int act;
            while (true)
            {
                act = IsValidInput(index - 1, 0);

                //나가기
                if (act == 0)
                {
                    break;
                }
                else if (act < index && act > 0)
                {
                    //마나가 부족한지 확인
                    if (player.Mp >= player.getSkillList[act-1].MP)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("마나가 부족합니다.");
                    }

                }
            }

            UseSkill(act);
        }

        //------------------------------------------------------------------------------------------------------
        //스킬 사용
        private void UseSkill(int act)
        {
            if(act == 0)
            {
                PlayerTurn();
                return;
            }

            //단일 타겟 스킬인 경우
            if (player.getSkillList[act - 1].Range == 1) 
            {

                //공격 행동 선택 시 타겟을 선택
                int target = SelectTarget();

                //아무것도 선택하지 않았을 때
                if (target == 0)
                {
                    //다시 되돌아 옴
                    PlayerSkillTurn();
                    return;
                }
                else
                {
                    //대상을 선택했다면, 스킬 공격
                    Console.Clear();
                    SkillAttackOne(player, battle_monster[target - 1], player.getSkillList[act - 1]);
                    player.Mp -= player.getSkillList[act - 1].MP;
                }


            }
            //무작위 타겟 스킬인 경우
            else if (player.getSkillList[act - 1].Range > 1)
            {
                SkillAttackRandom(player, player.getSkillList[act - 1]);

            }
            //전체 타겟 스킬인 경우
            else if (player.getSkillList[act - 1].Range == 0)
            {
                SkillAttackAll(player, player.getSkillList[act - 1]);

            }

            Console.WriteLine();
            Console.WriteLine(">> 다음");
            Console.ReadKey();
        }


        //---------------------------------------------------------------------------------------------------------------
        //타겟 1명 지정
        private int SelectTarget()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            int index = 1;
            foreach (Monster mon in battle_monster)
            {
                if (mon.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.WriteLine($"{index} Lv.{mon.Level} {mon.Name}   HP {mon.getHP}");
                Console.ResetColor();
                index++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}  ({player.GetJob})");
            Console.WriteLine($"HP {player.Health}");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("대상을 선택해 주세요.");

            int act;
            while (true)
            {
                act = IsValidInput(index - 1, 0);

                //나가기
                if (act == 0)
                {
                    break;
                }
                else if (act < index && act > 0)
                {
                    //몬스터를 선택
                    if (!battle_monster[act - 1].IsDead)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("이미 죽은 대상입니다.");
                    }

                }
            }

            return act;

        }


        //---------------------------------------------------------------------------------------------------------------
        //스킬 범위 1인 경우
        private void SkillAttackOne(ICharacter attacker, ICharacter victim, ISkill skill)
        {
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name} 의 {skill.Name}!");

            int damage = (int)((float)attacker.Ran_Attack() * skill.Coefficient) - victim.Total_Defence;
            if (damage < 0)
            {
                damage = 0;
            }

            int critical = random.Next(1, 100);

            if (critical <= 15)
            {
                damage = (int)Math.Ceiling((float)damage * 1.6);
                Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
            }
            else
            {
                Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
            }

            Console.WriteLine();

            int health = victim.Health;
            victim.TakeDamage(damage);

            Console.WriteLine($"Lv.{victim.Level} {victim.Name}");
            if (victim.IsDead)
            {
                Console.WriteLine($"HP {health} -> dead");
            }
            else
            {
                Console.WriteLine($"HP {health} -> {victim.Health}");
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //스킬 범위 무작위인 경우
        public void SkillAttackRandom(Player attacker, Skill skill)
        {
            List<Monster> victim = battle_monster.FindAll(x => !x.IsDead).OrderBy(_ => random.Next()).Take(skill.Range).ToList();
            player.Mp -= skill.MP;

            Console.Clear();
            Console.WriteLine($"{attacker.Name} 의 공격!");
            Console.WriteLine();

            foreach (Monster mon in victim)
            {
                int damage = (int)((float)attacker.Ran_Attack() * skill.Coefficient) - mon.Total_Defence;
                if (damage < 0)
                {
                    damage = 0;
                }

                int critical = random.Next(1, 100);

                if (critical <= 15)
                {
                    damage = (int)Math.Ceiling((float)damage * 1.6);
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                }


                int health = mon.Health;
                mon.TakeDamage(damage);

                Console.WriteLine($"Lv.{mon.Level} {mon.Name}");
                if (mon.IsDead)
                {
                    Console.WriteLine($"HP {health} -> dead");
                }
                else
                {
                    Console.WriteLine($"HP {health} -> {mon.Health}");
                }
                Console.WriteLine();
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //스킬 범위 전체인 경우
        public void SkillAttackAll(Player attacker, Skill skill)
        {
            player.Mp -= skill.MP;

            int critical = random.Next(1, 100);
            Console.Clear();
            Console.WriteLine($"{attacker.Name} 의 공격!");
            Console.WriteLine();

            foreach (Monster mon in battle_monster.FindAll(x => !x.IsDead))
            {
                int damage = (int)((float)attacker.Ran_Attack() * skill.Coefficient) - mon.Total_Defence;
                if (damage < 0)
                {
                    damage = 0;
                }

                if (critical <= 15)
                {
                    damage = (int)Math.Ceiling((float)damage * 1.6);
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                }


                int health = mon.Health;
                mon.TakeDamage(damage);

                Console.WriteLine($"Lv.{mon.Level} {mon.Name}");
                if (mon.IsDead)
                {
                    Console.WriteLine($"HP {health} -> dead");
                }
                else
                {
                    Console.WriteLine($"HP {health} -> {mon.Health}");
                }
                Console.WriteLine();
            }
        }


        //---------------------------------------------------------------------------------------------------------------
        //캐릭터 일반 공격 함수
        private void Attack(ICharacter attacker, ICharacter victim)
        {
            int miss = random.Next(0, 10);
            int critical = random.Next(0, 100);
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name} 의 공격!");

            int damage = attacker.Ran_Attack() - victim.Total_Defence;
            if (damage < 0)
            {
                damage = 0;
            }

            if (miss == 0)
            {
                Console.WriteLine($"{victim.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                damage = 0;
                return;
            }
            else
            {
                if (critical <= 15)
                {
                    damage = (int)Math.Ceiling((float)damage * 1.6);
                    Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                }
            }
            
            Console.WriteLine();

            int health = victim.Health;
            victim.TakeDamage(damage);

            Console.WriteLine($"Lv.{victim.Level} {victim.Name}");
            if (victim.IsDead)
            {
                Console.WriteLine($"HP {health} -> dead");
            }
            else
            {
                Console.WriteLine($"HP {health} -> {victim.Health}");
            }

        }

        //---------------------------------------------------------------------------------------------------------------
        //몬스터 행동 턴
        private void EnemyTurn(Monster character)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{character.Name} 이(가) 행동합니다.");
            Thread.Sleep(500);

            int useSkill;
            if (monsterList.getSkillList.FindAll(x => x.Type == character.Type).Count > 0)
            {
                useSkill = random.Next(1, 11);
            }
            else
            {
                useSkill = 0;
            }

            //보스몹일 때
            if (character.Type == MonsterType.BOSS_HERAID)
            {
                //스킬 사용 확률 40%
                if (useSkill >= 6)
                {
                    MonsterSkill skill = monsterList.getSkillList.FindAll(x => x.Type == character.Type).OrderBy(_ => random.Next()).ToList()[0];
                    SkillAttackOne(character, player, skill);
                }
                else
                {
                    Attack(character, player);
                }
            }
            else
            {
                //보스몹이 아닐 땐 스킬 사용 확률 20%
                if (useSkill >= 8)
                {
                    //스킬 리스트에서 스킬 하나를 무작위로 빼옴
                    MonsterSkill skill = monsterList.getSkillList.FindAll(x => x.Type == character.Type).OrderBy(_ => random.Next()).ToList()[0];
                    SkillAttackOne(character, player, skill);
                }
                else
                {
                    Attack(character, player);
                }
            }

            Console.WriteLine();
            Console.WriteLine(">> 다음");
            Console.ReadKey();
        }

        //---------------------------------------------------------------------------------------------------------------
        //입력이 올바른지 확인하는 함수
        public int IsValidInput(int max, int min)
        {
            int keyInput;
            bool result;
            int cnt = 0;

            do
            {
                if(cnt == 1)
                {
                    Console.WriteLine("다시 입력해 주세요.");
                    Console.Write(">>");
                }
                else
                {
                    Console.Write(">>");
                }
                result = int.TryParse(Console.ReadLine(), out keyInput);

                cnt = 1;
            } while (result == false || IsValidInput(keyInput, min, max) == false);

            return keyInput;
        }

        private bool IsValidInput(int keyInput, int min, int max)
        {
            if (min <= keyInput && keyInput <= max)
            {
                return true;
            }

            return false;
        }

        //---------------------------------------------------------------------------------------------------------------
        //중복 이름 체크
        private void CheckName(List<Monster> monsterList)
        {
            var list = monsterList.GroupBy(x => x.Name).Where(g => g.Count() > 1).Select(x => x.Key).ToList();

            foreach(string m in list)
            {
                int ascii = 65;
                for(int i = 0; i < monsterList.Count; i++)
                {
                    if (monsterList[i].Name == m)
                    {
                        monsterList[i].Name += "_" + (char)ascii;
                        ascii++;
                    }
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //던전 초기화
        public void ResetDungeon()
        {
            stage = 1;
        }

    }
}
