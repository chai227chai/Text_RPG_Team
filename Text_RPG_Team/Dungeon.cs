﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    [Serializable]
    internal class Dungeon
    {

        Player? player;
        MonsterList? monsterList;
        Portion hpPortion = new Portion(PortionType.HP, PortionValue.Small);
        Portion mpPortion = new Portion(PortionType.MP, PortionValue.Small);
        PortionList portionList;
        ItemList itemList = new ItemList();

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
        public void GoDungeon(Player player, PortionList portionList)
        {
            this.player = player;
            this.portionList = portionList;
            this.monsterList = new MonsterList(stage);

            this.battle_monster = new List<Monster>();

            player_health = player.Health;
            player_mp = player.Mp;

            int number = 1;
            if(stage >= 1 && stage <= 4)
            {
                number = new Random().Next(stage, stage + 1);
            }
            else if(stage >= 5 && stage <= 9)
            {
                number = new Random().Next(4, 9);
            }
            else if(stage == 10)
            {
                number = 1;
            }
            else if(stage > 10)
            {
                number = new Random().Next(stage, stage + 1);
            }

            int exp = 0;

            allCharacter = new ICharacter[number + 1];
            allCharacter[0] = player;

            for (int i = 0; i < number; i++)
            {
                int random_monster = new Random().Next(1, monsterList.GetMonsterList.Count+1);
                Monster newMonster = new Monster(monsterList.GetMonster(random_monster));
                battle_monster.Add(newMonster);

                allCharacter[i+1] = newMonster;
                exp += newMonster.DropExp;
            }
            stage_exp = exp;

            CheckName(battle_monster);

            StartPhase();
        }

        //---------------------------------------------------------------------------------------------------------------
        //던전 시작
        private void StartPhase()
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

            BattlePhase();
        }

        //---------------------------------------------------------------------------------------------------------------
        //전투 페이즈
        private void BattlePhase()
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
                allCharacter = allCharacter.OrderByDescending(x => x.RanSpeed()).ToArray();

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

                Console.WriteLine();
                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player_health} -> {player.Health}");

                int current_mp = player.Mp;
                player.Mp = player.Mp + 10 > player_mp ? player_mp : player.Mp + 10;
                Console.WriteLine($"MP {player_mp} -> {current_mp}");
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
            int reward_gold = 0;
            int reward_hp_potion = 0;
            int reward_mp_potion = 0;
            List<Item> reward_items = new List<Item>();

            foreach(Monster mon in battle_monster)
            {
                int i = new Random().Next(1, 5);
                reward_gold = 300 * mon.Level;
                if(i <= 1)
                {
                    reward_mp_potion += 1;
                }
                else if (1 < i && i <= 3)
                {
                    reward_hp_potion += 1;
                }

                string reward_item = mon.dropItem(mon);
                if (!reward_item.Equals("-1"))
                {
                    reward_items.Add(itemList.GetItemList.Find(itemnumber => itemnumber.Number == reward_item));
                }
            }
            player.Gold += reward_gold;
            hpPortion.SetPortion(reward_hp_potion);
            mpPortion.SetPortion(reward_mp_potion);
            Console.WriteLine();
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{reward_gold} Gold");
            if(reward_hp_potion >= 1)
            {
                Console.WriteLine($"체력 회복 포션 - {reward_hp_potion}");
                portionList.AddPortion(hpPortion.Type, hpPortion.Value, reward_hp_potion);
                hpPortion.Count = 0;
            }
            if(reward_mp_potion >= 1)
            {
                Console.WriteLine($"마나 회복 포션 - {reward_mp_potion}");
                portionList.AddPortion(mpPortion.Type, mpPortion.Value, reward_mp_potion);
                mpPortion.Count = 0;
            }
            foreach(Item item in reward_items)
            {
                player.GetInventory.AddInventroy(item);
                Console.WriteLine($"{item.Name}");
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
                Console.WriteLine($"Lv.{mon.Level} {mon.Name}   HP {mon.GetHP}  ATK {mon.Attack}");
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
            Console.WriteLine("3. 포션");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            int act = IsValidInput(3, 1);

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
            else if (act == 3)
            {
                Console.WriteLine();
                Console.WriteLine("사용하실 포션을 선택해 주세요.");
                Console.WriteLine("1.체력 포션\n2.마나 포션\n0.나가기");

                int i = IsValidInput(2, 0);
                switch (i)
                {
                    case 0:
                        PlayerTurn();
                        break;
                    case 1:
                        int list_lenght = portionList.UsePortionList(PortionType.HP);

                        int p_target = IsValidInput(list_lenght, 0);

                        if (p_target == 0)
                        {
                            PlayerTurn();
                            break;
                        }
                        Portion portion = portionList.GetPortion(PortionType.HP, p_target);
                        portionList.UsePortion(player, portion);
                        break;
                    case 2:
                        list_lenght = portionList.UsePortionList(PortionType.MP);

                        p_target = IsValidInput(list_lenght, 0);

                        if (p_target == 0)
                        {
                            PlayerTurn();
                            break;
                        }
                        portion = portionList.GetPortion(PortionType.MP, p_target);
                        portionList.UsePortion(player, portion);
                        break;
                }
                return;
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
                Console.WriteLine($"Lv.{mon.Level} {mon.Name}   HP {mon.GetHP}");
                Console.ResetColor();
                index++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}  ({player.GetJob})");
            Console.WriteLine($"HP {player.Health} / {player.MaxHealth}");
            Console.WriteLine($"MP {player.Mp} / {player.MaxMp}");
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
                    SkillAttackOne((Player)player, battle_monster[target - 1], player.getSkillList[act - 1]);
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
                Console.WriteLine($"{index} Lv.{mon.Level} {mon.Name}   HP {mon.GetHP}");
                Console.ResetColor();
                index++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}  ({player.GetJob})");
            Console.WriteLine($"HP {player.Health} / {player.MaxHealth}");

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
            skill.SkillAbility.Attacker = attacker;
            skill.SkillAbility.OnAttackStart();

            Console.WriteLine();
            Console.WriteLine($"{attacker.Name} 의 {skill.Name}!");

            int damage = (int)((float)attacker.RanAttack() * skill.Coefficient) - victim.TotalDefence;
            if (damage < 0)
            {
                damage = 0;
            }

            int critical = new Random().Next(0, 100);

            if (critical <= attacker.TotalCritRate)
            {
                damage = (int)Math.Ceiling((float)damage * attacker.TotalCritDMG);
                Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
            }
            else
            {
                Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
            }

            Console.WriteLine();

            int health = victim.Health;
            skill.SkillAbility.OnAttack(victim, damage);
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

            skill.SkillAbility.OnAttackEnd();
        }

        //---------------------------------------------------------------------------------------------------------------
        //스킬 범위 무작위인 경우
        public void SkillAttackRandom(Player attacker, Skill skill)
        {
            skill.SkillAbility.Attacker = attacker;
            skill.SkillAbility.OnAttackStart();

            List<Monster> victim = battle_monster.FindAll(x => !x.IsDead).OrderBy(_ => new Random().Next()).Take(skill.Range).ToList();
            player.Mp -= skill.MP;

            Console.Clear();
            Console.WriteLine($"{attacker.Name} 의 {skill.Name}!");
            Console.WriteLine();

            foreach (Monster mon in victim)
            {
                int damage = (int)((float)attacker.RanAttack() * skill.Coefficient) - mon.TotalDefence;
                if (damage < 0)
                {
                    damage = 0;
                }

                int critical = new Random().Next(0, 100);

                if (critical <= attacker.TotalCritRate)
                {
                    damage = (int)Math.Ceiling((float)damage * attacker.TotalCritDMG);
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                }


                int health = mon.Health;
                skill.SkillAbility.OnAttack(mon, damage);
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

            skill.SkillAbility.OnAttackEnd();
        }

        //---------------------------------------------------------------------------------------------------------------
        //스킬 범위 전체인 경우
        public void SkillAttackAll(Player attacker, Skill skill)
        {
            skill.SkillAbility.Attacker = attacker;
            skill.SkillAbility.OnAttackStart();

            player.Mp -= skill.MP;

            Console.Clear();
            Console.WriteLine($"{attacker.Name} 의 {skill.Name}!");
            Console.WriteLine();

            foreach (Monster mon in battle_monster.FindAll(x => !x.IsDead))
            {
                int damage = (int)((float)attacker.RanAttack() * skill.Coefficient) - mon.TotalDefence;
                if (damage < 0)
                {
                    damage = 0;
                }

                int critical = new Random().Next(0, 100);

                if (critical <= attacker.TotalCritRate)
                {
                    damage = (int)Math.Ceiling((float)damage * attacker.TotalCritDMG);
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                }


                int health = mon.Health;
                skill.SkillAbility.OnAttack(mon, damage);
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

            skill.SkillAbility.OnAttackEnd();
        }


        //---------------------------------------------------------------------------------------------------------------
        //캐릭터 일반 공격 함수
        private void Attack(ICharacter attacker, ICharacter victim)
        {
            int miss = new Random().Next(0, 100);
            int critical = new Random().Next(0, 100);
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name} 의 공격!");

            int damage = attacker.RanAttack() - victim.TotalDefence;
            if (damage < 0)
            {
                damage = 0;
            }

            if (miss <= victim.TotalEvasion)
            {
                Console.WriteLine($"{victim.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                damage = 0;
                return;
            }
            else
            {
                if (critical <= attacker.TotalCritRate)
                {
                    damage = (int)Math.Ceiling((float)damage * attacker.TotalCritDMG);
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

            int use_skill;

            if (monsterList.GetSkillList.FindAll(x => x.Type == character.Type).Count > 0)
            {
                use_skill = new Random().Next(1, 11);
            }
            else
            {
                use_skill = 0;
            }

            //보스몹일 때
            if (character.Type == MonsterType.BOSS_HERAID)
            {
                //스킬 사용 확률 40%
                if (use_skill >= 6)
                {
                    MonsterSkill skill = monsterList.GetSkillList.FindAll(x => x.Type == character.Type).OrderBy(_ => new Random().Next()).ToList()[0];
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
                if (use_skill >= 8)
                {
                    //스킬 리스트에서 스킬 하나를 무작위로 빼옴
                    MonsterSkill skill = monsterList.GetSkillList.FindAll(x => x.Type == character.Type).OrderBy(_ => new Random().Next()).ToList()[0];
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
            int key_input;
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
                result = int.TryParse(Console.ReadLine(), out key_input);

                cnt = 1;
            } while (result == false || IsValidInput(key_input, min, max) == false);

            return key_input;
        }

        private bool IsValidInput(int key_input, int min, int max)
        {
            if (min <= key_input && key_input <= max)
            {
                return true;
            }

            return false;
        }

        //---------------------------------------------------------------------------------------------------------------
        //중복 이름 체크
        private void CheckName(List<Monster> monster_list)
        {
            var list = monster_list.GroupBy(x => x.Name).Where(g => g.Count() > 1).Select(x => x.Key).ToList();

            foreach(string m in list)
            {
                int ascii = 65;
                for(int i = 0; i < monster_list.Count; i++)
                {
                    if (monster_list[i].Name == m)
                    {
                        monster_list[i].Name += "_" + (char)ascii;
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
