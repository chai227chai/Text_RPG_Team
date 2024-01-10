﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        List<ICharacter>? battle_monster;
        ICharacter[]? allCharacter;

        int player_health;
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

            this.battle_monster = new List<ICharacter>();

            player_health = player.Health;

            int number = 1;
            if(stage >= 1 && stage < 4)
            {
                number = random.Next(stage, stage + 1);
            }
            else if(stage >= 5 && stage < 9)
            {
                number = random.Next(3, 6);
            }
            else if(stage == 10)
            {
                number = 1;
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

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            int act = IsValidInput(0, 0);

            if (act == 0)
            {
                Battle_phase();
                return;
            }

        }

        //---------------------------------------------------------------------------------------------------------------
        private void Battle_phase()
        {
            int death_cnt = 0;
            int turn = 1;

            while (!player.IsDead && death_cnt < battle_monster.Count)
            {
                death_cnt = 0;

                //턴 시작 시 마다 스피드 별로 정렬
                allCharacter = allCharacter.OrderByDescending(x => x.SetSpeed()).ToArray();

                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                Console.WriteLine($"{turn} 번째 턴");
                Console.WriteLine();
                Thread.Sleep(500);

                foreach (ICharacter character in allCharacter)
                {
                    //캐릭이 플레이어일 때
                    if(character.Tag == CHAR_TAG.PLAYER && !character.IsDead)
                    {
                        Console.Clear();
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
                        EnemyTurn(character);
                    }
                }

                foreach(Monster mon in battle_monster)
                {
                    if (mon.IsDead)
                    {
                        death_cnt++;
                    }
                }
                if (death_cnt >= battle_monster.Count)
                {
                    break;
                }

                turn++;
            }

            Result();
        }

        //---------------------------------------------------------------------------------------------------------------

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

                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player_health} -> {player.Health}");

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
            Console.WriteLine($"HP {player.Health}");

            Console.WriteLine();
            Console.WriteLine("1. 공격");

            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            int act = IsValidInput(1, 1);

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
                    Console.Clear();
                    Attack(player, battle_monster[target - 1]);
                }
                Console.WriteLine();
                Console.WriteLine("0. 다음");

                int act2 = IsValidInput(0, 0);

                if (act2 == 0)
                {
                    return;
                }

            }
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
        private void EnemyTurn(ICharacter character)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{character.Name} 이(가) 행동합니다.");
            Thread.Sleep(500);

            Attack(character, player);
 
            Console.WriteLine();
            Console.WriteLine("0. 다음");

            int act = IsValidInput(0, 0);

            if (act == 0)
            {
                return;
            }
        }


        //---------------------------------------------------------------------------------------------------------------

        private void Attack(ICharacter attacker, ICharacter victim)
        {
            int miss = random.Next(1, 10);
            int critical = random.Next(1, 100);
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name} 의 공격!");

            int damage = Damage_check(attacker.Attack) - victim.Defence;
            if (damage < 0)
            {
                damage = 0;
            }

            if (miss <= 1)
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
        private int Damage_check(int attack)
        {
            int damage_range = (int)Math.Ceiling((float)attack * 0.1) ;
            int damage = random.Next(attack - damage_range, attack + damage_range + 1) ;
            return damage;
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
    }
}
