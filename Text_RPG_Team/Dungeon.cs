using System;
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

        int player_health;
        int drop_exp = 0;

        public void GoDungeon(Player player)
        {
            this.player = player;
            this.monsterList = new MonsterList();

            this.battle_monster = new List<ICharacter>();

            player_health = player.Health;

            int number = random.Next(1, 5);

            for(int i = 0; i < number; i++)
            {
                int random_monster = random.Next(1, monsterList.getMonsterList.Count+1);
                Monster newMonster = new Monster(monsterList.getMonster(random_monster));
                drop_exp += newMonster.Drop_Exp;
                battle_monster.Add(newMonster);
            }

            Battle_phase();
        }

        private void Battle_phase()
        {
            int death_cnt = 0;

            while (!player.IsDead && death_cnt < battle_monster.Count)
            {
                death_cnt= 0;

                PlayerTurn();

                PlayerAttackTurn();

                foreach (Monster mon in battle_monster)
                {
                    if (mon.IsDead)
                    {
                        death_cnt++;
                    }
                }

                if(death_cnt < battle_monster.Count)
                {
                    EnemyTurn();
                }
                
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
                drop_exp = 0;

                Console.WriteLine("You Lose");
                Console.WriteLine();

                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player_health} -> {player.Health}");

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
                player.LevelUp(drop_exp);
                drop_exp = 0;

                Console.WriteLine("Victory");
                Console.WriteLine();

                Console.WriteLine($"던전에서 몬스터 {battle_monster.Count}마리를 잡았습니다.");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player_health} -> {player.Health}");

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

            Console.WriteLine("당신의 차례입니다.");
            Console.WriteLine();
            Thread.Sleep(500);

            int index = 1;
            foreach (Monster mon in battle_monster)
            {
                Console.WriteLine($"Lv.{mon.Level} {mon.Name}   HP {mon.getHP}");
                index++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}  ({player.GetJob})");
            Console.WriteLine($"HP {player.Health}");

            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            int act = IsValidInput(1, 1);

            if(act == 1)
            {
                return;
            }
        }

        private void PlayerAttackTurn()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            int index = 1;
            foreach (Monster mon in battle_monster)
            {
                Console.WriteLine($"{index} Lv.{mon.Level} {mon.Name}   HP {mon.getHP}");
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

            bool loop = true;
            while (loop)
            {
                int act = IsValidInput(index - 1, 0);

                if (act == 0)
                {
                    break;
                }
                else if (act < index && act > 0)
                {
                    if (!battle_monster[act - 1].IsDead)
                    {
                        float damage = Damage_check(player.Attack);
                        Console.Clear();
                        Attack(player, battle_monster[act - 1], damage);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("이미 죽은 대상입니다.");
                    }

                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            int act2 = IsValidInput(0, 0);

            if (act2 == 0)
            {
                return;
            }

        }

        //---------------------------------------------------------------------------------------------------------------
        private void EnemyTurn()
        {
            Console.Clear();
            Console.WriteLine("적들이 공격합니다.");
            Console.WriteLine();
            Thread.Sleep(500);

            foreach (Monster mon in battle_monster)
            {
                if (!mon.IsDead)
                {
                    float damage = Damage_check(mon.Attack);
                    
                    Attack(mon, player, damage);
                    Thread.Sleep(200);
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            int act = IsValidInput(0, 0);

            if (act == 0)
            {
                return;
            }

        }

        //---------------------------------------------------------------------------------------------------------------

        private void Attack(ICharacter attacker, ICharacter victim, float damage)
        {
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name} 의 공격!");
            Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
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
        private float Damage_check(float attack)
        {
            float damage_range = (float)Math.Ceiling((float)attack * 0.1) ;
            float damage = random.Next((int)(attack - damage_range)/10, (int)(attack + damage_range + 1));

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
