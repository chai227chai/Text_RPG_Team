using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Text_RPG_Team
{
    internal class GameManager
    {
        TextEdit textedit = new TextEdit();
        Player character = new Player();
        Dungeon dungeon = new Dungeon();
        Portion hpportion = new Portion(PortionType.HP);
        Portion mpportion = new Portion(PortionType.MP);
        ItemList itemlist = new ItemList();
        Store store = new Store();
        Inventory inventory = new Inventory();
        public GameManager()
        {
            FirstScreen();
            character.Name = SetCharacter();
            SetJob();
            MainTown();
        }

        //---------------------------------------------------------------------------------------------------------------
        //게임 시작 초기 화면 함수
        private void FirstScreen()
        {
            Console.WriteLine("=============================================================================================");
            Console.WriteLine(" /$$$$$$$$/$$$$$$$$ /$$   /$$ /$$$$$$$$        /$$$$$$   /$$$$$$  /$$      /$$ /$$$$$$$$");
            Console.WriteLine("|__  $$__/ $$_____/| $$  / $$|__  $$__/       /$$__  $$ /$$__  $$| $$$    /$$$| $$_____/");
            Console.WriteLine("   | $$  | $$      |  $$/ $$/   | $$         | $$  \\__/| $$  \\ $$| $$$$  /$$$$| $$      ");
            Console.WriteLine("   | $$  | $$$$$    \\  $$$$/    | $$         | $$ /$$$$| $$$$$$$$| $$ $$/$$ $$| $$$$$   ");
            Console.WriteLine("   | $$  | $$__/     >$$  $$    | $$         | $$|_  $$| $$__  $$| $$  $$$| $$| $$__/   ");
            Console.WriteLine("   | $$  | $$       /$$/\\  $$   | $$         | $$  \\ $$| $$  | $$| $$\\  $ | $$| $$      ");
            Console.WriteLine("   | $$  | $$$$$$$$| $$  \\ $$   | $$         |  $$$$$$/| $$  | $$| $$ \\/  | $$| $$$$$$$$");
            Console.WriteLine("   |__/  |________/|__/  |__/   |__/          \\______/ |__/  |__/|__/     |__/|________/");
            Console.WriteLine("=============================================================================================");
            Console.WriteLine(textedit.PadLeftForMixedText("Press Any Key to Play", 93));
            Console.WriteLine("=============================================================================================");
            Console.ReadKey();
        }

        //---------------------------------------------------------------------------------------------------------------
        public string SetCharacter()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">>");
            return Console.ReadLine();
        }
        public void SetJob() //직업선택후 직업에 따른스탯변경
        {
            string setJob = "초보자";
            Console.Clear();
            Console.WriteLine("캐릭터의 직업을 선택해 주세요");
            Console.WriteLine();
            Console.WriteLine("1.전사 2.마법사 3.도적");

            Console.WriteLine();
          
            int chooseJob = IsValidInput(3, 1);
            switch(chooseJob)
            {
                case 1:
                    character.Job = JOB.WARRIOR;
                    character.MaxHealth = 200;
                    character.Health = character.MaxHealth;
                    character.MaxMp = 50;
                    character.Mp = character.MaxMp;
                    character.Attack = 5;
                    character.Defence = 10;
                    character.Speed = 3;
                    setJob = character.GetJob;
                    UseSkill(setJob);
                    break;
                case 2:
                    character.Job = JOB.WIZARD;
                    character.MaxHealth = 100;
                    character.Health = character.MaxHealth;
                    character.MaxMp = 200;
                    character.Mp = character.MaxMp;
                    character.Attack = 10;
                    character.Defence = 5;
                    character.Speed = 2;
                    setJob = character.GetJob;
                    UseSkill(setJob);
                    break;
                case 3:
                    character.Job = JOB.ROGUE;
                    character.MaxHealth = 150;
                    character.Health = character.MaxHealth;
                    character.MaxMp = 100;
                    character.Mp = character.MaxMp;
                    character.Attack = 8;
                    character.Defence = 8;
                    character.Speed = 5;
                    setJob = character.GetJob;
                    UseSkill(setJob);
                    break;
            }
            Console.Clear();
            Console.WriteLine($"당신의 직업은 {setJob} 입니다. 확정하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 예 2.아니오");
            Console.WriteLine();

            int choose = IsValidInput(2, 1);
            switch (choose)
            {
                case 1:
                    character.Gold = 1500;
                    hpportion.SetHpPortion(3);
                    break;
                case 2:
                    SetJob();
                    break;
            }

        }

        //---------------------------------------------------------------------------------------------------------------
        public void MainTown()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine();

            Console.WriteLine("1. 상태보기");
            Console.WriteLine($"2. 전투시작  (현재 층 수 : {dungeon.Now_Stage}층)");
            Console.WriteLine("3. 회복 아이템");
            Console.WriteLine("4. 인벤토리");
            Console.WriteLine("5. 상점");

            Console.WriteLine();

            int act = IsValidInput(5, 1);

            switch (act)
            {
                case 1:
                    ViewPlayer();
                    break;
                case 2:
                    EnterDungeon();
                    break;
                case 3:
                    ViewPortion();
                    break;
                case 4:
                    ViewInventory();
                    break;
                case 5:
                    ViewStore();
                    break;
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        private void EnterDungeon()
        {
            dungeon.GoDungeon(character);

            MainTown();
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
                if (cnt == 0)
                {
                    Console.WriteLine("원하시는 행동을 입력해 주세요.");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("다시 입력해 주세요.");
                }
                Console.Write(">>");
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
        //선택하는 화면으로 이동하는 함수
        //1. 상태보기
        private void ViewPlayer()
        {
            Console.Clear();
            Console.WriteLine("■상태보기■");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("LV : " + character.Level.ToString("00"));
            Console.WriteLine($"{character.Name} ( {character.GetJob} )");
            Console.Write($"공격력 : {character.Attack}");
            if (character.CheckAttack)
            {
                Console.WriteLine($" (+{character.PlusAttack})");
            }
            else Console.WriteLine();
            Console.Write($"방어력 : {character.Defence}");
            if (character.CheckDefence)
            {
                Console.WriteLine($" (+{character.PlusDefence})");
            }
            else Console.WriteLine();
            Console.WriteLine($"체  력 : {character.Health}");
            Console.WriteLine($"마  나 : {character.Mp}");
            Console.WriteLine($"Gold : {character.Gold}G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(0, 0);

            switch (act)
            {
                case 0:
                    break;
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //3. 회복 아이템
        private void ViewPortion()
        {
            Console.Clear();
            Console.WriteLine("■회복■");
            Console.WriteLine($"포션을 사용하면 체력을 30 회복할 수 있습니다. (남은 포션 : {hpportion.HpCount})");
            Console.WriteLine();
            Console.WriteLine($"회복 가능한 최대 체력 : {character.MaxHealth}");
            Console.WriteLine($"현재 체력 : {character.Health}");
            Console.WriteLine();
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(1, 0);
            switch (act)
            {
                case 0:
                    break;
                case 1:
                    hpportion.UseHpPortion(character);
                    break;
            }
        }
      
        //---------------------------------------------------------------------------------------------------------------
        //4. 인벤토리
        private void ViewInventory()
        {
            Console.Clear();
            Console.WriteLine("■인벤토리■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            itemlist.PrintItemList(inventory.GetInventoryList);
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(1, 0);

            switch (act)
            {
                case 0:
                    break;
                case 1:
                    InventoryManager();
                    break;
            }
        }

        private void InventoryManager()
        {
            Console.Clear();
            Console.WriteLine("■인벤토리 - 장착 관리■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            itemlist.PrintItemList(inventory.GetInventoryList, true);
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            int act = IsValidInput(inventory.GetInventoryList.Count, 0);

            if (act == 0)
            {
                ViewInventory();
                return;
            }
            else
            {
                Item selectitem = inventory.GetItem(act);
                inventory.SetPlayerSpec(selectitem.Type, selectitem, character);
                InventoryManager();
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //5. 상점
        private void ViewStore()
        {
            Console.Clear();
            Console.WriteLine("■상점■");
            Console.WriteLine("필요한 아이템을 얻거나 가지고 있는 아이템을 팔 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold}G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            itemlist.PrintItemList(itemlist.GetItemList, false ,true);
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(1, 0);

            switch (act)
            {
                case 0:
                    break;
                case 1:
                    StoreBuy();
                    break;
            }
        }

        //구매
        private void StoreBuy()
        {
            Console.Clear();
            Console.WriteLine("■상점 - 아이템 구매■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold}G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            itemlist.PrintItemList(itemlist.GetItemList, true, true);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(itemlist.itemnumber, 0);

            if (act == 0)
            {
                ViewStore();
                return;
            }
            else
            {
                Item solditem = store.BuyItem(act, itemlist, character.Gold);
                if(solditem != null)
                {
                    character.Gold -= solditem.Price;
                    inventory.addInventroy(solditem);
                    StoreBuy();
                }
                else
                {
                    ViewStore();
                }
            }
        }
      
        //스킬 부여 함수
        static void UseSkill(string setJob)
        {
            Skill._skills = new Skill[10];

            if (setJob == "전사")
            {
                Skill.AddSkill(new Skill("파워 스트라이크", "한 명의 적에게 강한 데미지를 가합니다.", 5, 3, 1));
                Skill.AddSkill(new Skill("슬래시 블러스트", "모든 적에게 데미지를 가합니다.", 10, 2, 2));
            }
            else if (setJob == "마법사")
            {
                Skill.AddSkill(new Skill("에너지 볼트", "한 명의 적에게 데미지를 입힙니다.", 20, 2, 1));
                Skill.AddSkill(new Skill("메테오 스트라이크", "모든 적에게 강력한 데미지를 입힙니다.", 100, 5, 2));
            }
            else if (setJob == "도적")
            {
                Skill.AddSkill(new Skill("부식", "한 명의 적에게 데미지를 줍니다.", 10, 2, 1));
                Skill.AddSkill(new Skill("암살", "한 명의 적에게 치명적인 데미지를 줍니다.", 100, 10, 1));
            }
        }
    }
}
