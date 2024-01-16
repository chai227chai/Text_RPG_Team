using System;
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
    internal class dungeon
    {

        Player? Player;
        MonsterList? MonsterList;
        Portion HPportion = new Portion(PortionType.HP, PortionValue.Small);
        Portion MPportion = new Portion(PortionType.MP, PortionValue.Small);
        PortionList PortionList;
        ItemList ItemList = new ItemList();

        List<Monster>? BattleMonster;
        ICharacter[]? AllCharacter;

        int PlayerHealth;
        int PlayerMp;
        int StageExp;
        int Stage;


        public dungeon()
        {
            Stage = 1;
        }

        public int Now_Stage
        {
            get { return Stage; }
        }

        //던전 생성
        public void goDungeon(Player player, PortionList portionList)
        {
            this.Player = player;
            this.PortionList = portionList;
            this.MonsterList = new MonsterList(Stage);

            this.BattleMonster = new List<Monster>();

            PlayerHealth = player.Health;
            PlayerMp = player.Mp;

            int number = 1;
            if(Stage >= 1 && Stage <= 4)
            {
                number = new Random().Next(Stage, Stage + 1);
            }
            else if(Stage >= 5 && Stage <= 9)
            {
                number = new Random().Next(4, 9);
            }
            else if(Stage == 10)
            {
                number = 1;
            }
            else if(Stage > 10)
            {
                number = new Random().Next(Stage, Stage + 1);
            }

            int exp = 0;

            AllCharacter = new ICharacter[number + 1];
            AllCharacter[0] = player;

            for (int i = 0; i < number; i++)
            {
                int random_monster = new Random().Next(1, MonsterList.getMonsterList.Count+1);
                Monster newMonster = new Monster(MonsterList.getMonster(random_monster));
                BattleMonster.Add(newMonster);

                AllCharacter[i+1] = newMonster;
                exp += newMonster.DropExp;
            }
            StageExp = exp;

            checkName(BattleMonster);

            startPhase();
        }

        //---------------------------------------------------------------------------------------------------------------
        //던전 시작
        private void startPhase()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Console.WriteLine($"{Stage}층에 입장합니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            
            Console.WriteLine("몬스터들이 당신의 앞을 막아섭니다.");
            Console.WriteLine();
            foreach (Monster mon in BattleMonster)
            {
                Console.WriteLine($"Lv.{mon.Level} {mon.Name} ");
            }
            Console.WriteLine();
            Console.WriteLine(">> 다음");
            Console.ReadKey();

            battlePhase();
        }

        //---------------------------------------------------------------------------------------------------------------
        //전투 페이즈
        private void battlePhase()
        {
            int death_cnt = 0;
            int turn = 0;

            while (!Player.IsDead && death_cnt < BattleMonster.Count)
            {
                //죽은 몬스터 수
                death_cnt = 0;

                //턴 수 증가
                turn++;

                //턴 시작 시 마다 스피드 별로 정렬
                AllCharacter = AllCharacter.OrderByDescending(x => x.RanSpeed()).ToArray();

                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                Console.WriteLine($"{turn} 번째 턴");
                Console.WriteLine();
                Thread.Sleep(500);

                //모든 캐릭 한번 씩 돌아가며 행동
                foreach (ICharacter character in AllCharacter)
                {
                    //캐릭이 플레이어일 때
                    if(character.Tag == CHAR_TAG.PLAYER && !character.IsDead)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("당신의 차례입니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);

                        playerTurn();
                    }
                    else if(character.Tag == CHAR_TAG.PLAYER && character.IsDead)
                    {
                        break;
                    }
                    //캐릭이 몬스터일 때
                    else if(character.Tag == CHAR_TAG.MONSTER && !character.IsDead)
                    {
                        enemyTurn((Monster)character);
                    }
                }

                //죽은 몬스터 수 체크해서 던전 클리어 확인
                foreach(Monster mon in BattleMonster)
                {
                    if (mon.IsDead)
                    {
                        death_cnt++;
                    }
                }

            }

            result();
        }

        //---------------------------------------------------------------------------------------------------------------
        //던전 탐험 결과
        private void result()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - result");
            Console.WriteLine();

            if (Player.IsDead)
            {
                Console.WriteLine("You Lose");
                Console.WriteLine();

                Console.WriteLine($"Lv.{Player.Level} {Player.Name}");
                Console.WriteLine($"HP {PlayerHealth} -> {Player.Health}");

                Console.WriteLine();
                Console.WriteLine("당신은 던전에서 도망쳤습니다.");
                Stage = 1;

                Console.WriteLine();
                Console.WriteLine("0. 다음");

                int act = isValidInput(0, 0);

                if (act == 0)
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("Victory");
                Console.WriteLine();

                Console.WriteLine($"던전에서 몬스터 {BattleMonster.Count}마리를 잡았습니다.");
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{Player.Level} {Player.Name}");
                Console.WriteLine($"HP {PlayerHealth} -> {Player.Health}");

                int currentMp = Player.Mp;
                Player.Mp = Player.Mp + 10 > PlayerMp ? PlayerMp : Player.Mp + 10;
                Console.WriteLine($"MP {PlayerMp} -> {currentMp}");
                Console.WriteLine($"MP 회복(10) -> {Player.Mp}");

                Console.WriteLine();
                rewards();

                Console.WriteLine();
                Player.LevelUp(StageExp);

                Console.WriteLine();
                Console.WriteLine("다음 층으로 올라갑니다.");
                Stage++;

                Console.WriteLine();
                Console.WriteLine("0. 다음");

                int act = isValidInput(0, 0);

                if (act == 0)
                {
                    return;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        //몬스터 보상계산
        private void rewards()
        {
            int rewardGold = 0;
            int rewardHpPotion = 0;
            int rewardMpPotion = 0;
            List<Item> reward_items = new List<Item>();

            foreach(Monster mon in BattleMonster)
            {
                int i = new Random().Next(1, 5);
                rewardGold = 300 * mon.Level;
                if(i <= 1)
                {
                    rewardMpPotion += 1;
                }
                else if (1 < i && i <= 3)
                {
                    rewardHpPotion += 1;
                }

                string reward_item = mon.dropItem(mon);
                if (!reward_item.Equals("-1"))
                {
                    reward_items.Add(ItemList.GetItemList.Find(itemnumber => itemnumber.Number == reward_item));
                }
            }
            Player.Gold += rewardGold;
            HPportion.SetPortion(rewardHpPotion);
            MPportion.SetPortion(rewardMpPotion);
            Console.WriteLine();
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{rewardGold} Gold");
            if(rewardHpPotion >= 1)
            {
                Console.WriteLine($"체력 회복 포션 - {rewardHpPotion}");
                PortionList.AddPortion(HPportion.Type, HPportion.Value, rewardHpPotion);
                HPportion.Count = 0;
            }
            if(rewardMpPotion >= 1)
            {
                Console.WriteLine($"마나 회복 포션 - {rewardMpPotion}");
                PortionList.AddPortion(MPportion.Type, MPportion.Value, rewardMpPotion);
                MPportion.Count = 0;
            }
            foreach(Item item in reward_items)
            {
                Player.GetInventory.addInventroy(item);
                Console.WriteLine($"{item.Name}");
            }
        }


        //---------------------------------------------------------------------------------------------------------------
        //플레이어 행동 턴
        private void playerTurn()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            int index = 1;
            foreach (Monster mon in BattleMonster)
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
            Console.WriteLine($"Lv.{Player.Level} {Player.Name}  ({Player.GetJob})");
            Console.WriteLine();
            Console.WriteLine($"HP {Player.Health} / {Player.MaxHealth}");
            Console.WriteLine($"MP {Player.Mp} / {Player.MaxMp}");

            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 포션");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            int act = isValidInput(3, 1);

            int target;
            if(act == 1)
            {
                //공격 행동 선택 시 타겟을 선택
                target = selectTarget();

                //아무것도 선택하지 않았을 때
                if(target == 0)
                {
                    //다시 되돌아 옴
                    playerTurn();
                    return;
                }
                else
                {
                    //대상을 선택했다면, 공격
                    Console.Clear();
                    attack(Player, BattleMonster[target - 1]);
                }
                Console.WriteLine();
                Console.WriteLine(">> 다음");
                Console.ReadKey();

            }
            else if (act == 2)
            {
                playerSkillTurn();
            }
            else if (act == 3)
            {
                Console.WriteLine();
                Console.WriteLine("사용하실 포션을 선택해 주세요.");
                Console.WriteLine("1.체력 포션\n2.마나 포션\n0.나가기");

                int i = isValidInput(2, 0);
                switch (i)
                {
                    case 0:
                        playerTurn();
                        break;
                    case 1:
                        int listlenght = PortionList.UsePortionList(PortionType.HP);

                        int p_target = isValidInput(listlenght, 0);

                        if (p_target == 0)
                        {
                            playerTurn();
                            break;
                        }
                        Portion portion = PortionList.GetPortion(PortionType.HP, p_target);
                        PortionList.UsePortion(Player, portion);
                        break;
                    case 2:
                        listlenght = PortionList.UsePortionList(PortionType.MP);

                        p_target = isValidInput(listlenght, 0);

                        if (p_target == 0)
                        {
                            playerTurn();
                            break;
                        }
                        portion = PortionList.GetPortion(PortionType.MP, p_target);
                        PortionList.UsePortion(Player, portion);
                        break;
                }
                return;
            }
        }
        //---------------------------------------------------------------------------------------------------
        //스킬 선택 시 턴
        private void playerSkillTurn()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            int index = 1;
            foreach (Monster mon in BattleMonster)
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
            Console.WriteLine($"Lv.{Player.Level} {Player.Name}  ({Player.GetJob})");
            Console.WriteLine($"HP {Player.Health} / {Player.MaxHealth}");
            Console.WriteLine($"MP {Player.Mp} / {Player.MaxMp}");
            Console.WriteLine();

            index = 1;
            foreach(Skill skill in Player.getSkillList)
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
                act = isValidInput(index - 1, 0);

                //나가기
                if (act == 0)
                {
                    break;
                }
                else if (act < index && act > 0)
                {
                    //마나가 부족한지 확인
                    if (Player.Mp >= Player.getSkillList[act-1].MP)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("마나가 부족합니다.");
                    }

                }
            }

            useSkill(act);
        }

        //------------------------------------------------------------------------------------------------------
        //스킬 사용
        private void useSkill(int act)
        {
            if(act == 0)
            {
                playerTurn();
                return;
            }

            //단일 타겟 스킬인 경우
            if (Player.getSkillList[act - 1].Range == 1) 
            {

                //공격 행동 선택 시 타겟을 선택
                int target = selectTarget();

                //아무것도 선택하지 않았을 때
                if (target == 0)
                {
                    //다시 되돌아 옴
                    playerSkillTurn();
                    return;
                }
                else
                {
                    //대상을 선택했다면, 스킬 공격
                    Console.Clear();
                    skillAttackOne((Player)Player, BattleMonster[target - 1], Player.getSkillList[act - 1]);
                    Player.Mp -= Player.getSkillList[act - 1].MP;
                }


            }
            //무작위 타겟 스킬인 경우
            else if (Player.getSkillList[act - 1].Range > 1)
            {
                skillAttackRandom(Player, Player.getSkillList[act - 1]);

            }
            //전체 타겟 스킬인 경우
            else if (Player.getSkillList[act - 1].Range == 0)
            {
                skillAttackAll(Player, Player.getSkillList[act - 1]);

            }

            Console.WriteLine();
            Console.WriteLine(">> 다음");
            Console.ReadKey();
        }


        //---------------------------------------------------------------------------------------------------------------
        //타겟 1명 지정
        private int selectTarget()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            int index = 1;
            foreach (Monster mon in BattleMonster)
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
            Console.WriteLine($"Lv.{Player.Level} {Player.Name}  ({Player.GetJob})");
            Console.WriteLine($"HP {Player.Health} / {Player.MaxHealth}");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("대상을 선택해 주세요.");

            int act;
            while (true)
            {
                act = isValidInput(index - 1, 0);

                //나가기
                if (act == 0)
                {
                    break;
                }
                else if (act < index && act > 0)
                {
                    //몬스터를 선택
                    if (!BattleMonster[act - 1].IsDead)
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
        private void skillAttackOne(ICharacter attacker, ICharacter victim, ISkill skill)
        {
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
        public void skillAttackRandom(Player attacker, Skill skill)
        {
            List<Monster> victim = BattleMonster.FindAll(x => !x.IsDead).OrderBy(_ => new Random().Next()).Take(skill.Range).ToList();
            Player.Mp -= skill.MP;

            Console.Clear();
            Console.WriteLine($"{attacker.Name} 의 공격!");
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
        public void skillAttackAll(Player attacker, Skill skill)
        {
            Player.Mp -= skill.MP;

            Console.Clear();
            Console.WriteLine($"{attacker.Name} 의 공격!");
            Console.WriteLine();

            foreach (Monster mon in BattleMonster.FindAll(x => !x.IsDead))
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
        private void attack(ICharacter attacker, ICharacter victim)
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
        private void enemyTurn(Monster character)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{character.Name} 이(가) 행동합니다.");
            Thread.Sleep(500);

            int useSkill;
            if (MonsterList.getSkillList.FindAll(x => x.Type == character.Type).Count > 0)
            {
                useSkill = new Random().Next(1, 11);
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
                    MonsterSkill skill = MonsterList.getSkillList.FindAll(x => x.Type == character.Type).OrderBy(_ => new Random().Next()).ToList()[0];
                    skillAttackOne(character, Player, skill);
                }
                else
                {
                    attack(character, Player);
                }
            }
            else
            {
                //보스몹이 아닐 땐 스킬 사용 확률 20%
                if (useSkill >= 8)
                {
                    //스킬 리스트에서 스킬 하나를 무작위로 빼옴
                    MonsterSkill skill = MonsterList.getSkillList.FindAll(x => x.Type == character.Type).OrderBy(_ => new Random().Next()).ToList()[0];
                    skillAttackOne(character, Player, skill);
                }
                else
                {
                    attack(character, Player);
                }
            }

            Console.WriteLine();
            Console.WriteLine(">> 다음");
            Console.ReadKey();
        }

        //---------------------------------------------------------------------------------------------------------------
        //입력이 올바른지 확인하는 함수
        public int isValidInput(int max, int min)
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
            } while (result == false || isValidInput(keyInput, min, max) == false);

            return keyInput;
        }

        private bool isValidInput(int keyInput, int min, int max)
        {
            if (min <= keyInput && keyInput <= max)
            {
                return true;
            }

            return false;
        }

        //---------------------------------------------------------------------------------------------------------------
        //중복 이름 체크
        private void checkName(List<Monster> monsterList)
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
        public void resetDungeon()
        {
            Stage = 1;
        }

    }
}
