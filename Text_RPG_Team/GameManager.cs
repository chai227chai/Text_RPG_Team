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
        Player character;
        Dungeon dungeon = new Dungeon();
        PortionList portionlist = new PortionList();
        ItemList itemlist = new ItemList();
        Store store = new Store();
        Inventory inventory = new Inventory();

        string name;

        public GameManager()
        {
            FirstScreen();
            //character.Name = SetCharacter();
            name = SetCharacter();
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
            textedit.ChangeTextColorCyan(textedit.PadLeftForMixedText("Press Any Key to Play", 93));
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
            Console.Clear();
            Console.WriteLine("캐릭터의 직업을 선택해 주세요");
            Console.WriteLine();
            Console.WriteLine("1.전사 2.마법사 3.도적");

            Console.WriteLine();
          
            int chooseJob = IsValidInput(3, 1);
            switch(chooseJob)
            {
                case 1:
                    character = new Player(name, JOB.WARRIOR, 200, 50, 5, 10, 3);
                    break;
                case 2:
                    character = new Player(name, JOB.WIZARD, 100, 200, 10, 5, 2);
                    break;
                case 3:
                    character = new Player(name, JOB.ROGUE, 150, 100, 8, 8, 5);
                    break;
            }
            Console.Clear();
            Console.WriteLine($"당신의 직업은 {character.GetJob} 입니다. 확정하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 예 2.아니오");
            Console.WriteLine();

            int choose = IsValidInput(2, 1);
            switch (choose)
            {
                case 1:
                    character.Gold = 1500;
                    character.UseSkill();
                    portionlist.AddPortion(PortionType.HP, PortionValue.Small, 3);
                    portionlist.AddPortion(PortionType.MP, PortionValue.Small, 3);
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
                    //dungeon.PlusPortion(hpportion);
                    //dungeon.PlusPortion(mpportion);
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
            Console.Write($"공격력 : {character.Total_Attack}");
            Console.WriteLine((character.PlusAttack != 0) ? $" ( {(character.PlusAttack > 0 ? "+" : "")} {character.PlusAttack})" : "");
            Console.Write($"방어력 : {character.Total_Defence}");
            Console.WriteLine((character.PlusDefence != 0) ? $" ( {(character.PlusDefence > 0 ? "+" : "")} {character.PlusDefence})" : "");
            Console.WriteLine($"체  력 : {character.Health}");
            Console.WriteLine($"마  나 : {character.Mp}");
            Console.Write($"속  도 : {character.Total_Speed}");
            Console.WriteLine((character.PlusSpeed != 0) ? $" ( {(character.PlusSpeed > 0 ? "+" : "")} {character.PlusSpeed})" : "");
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
        //2. 던전 입장
        private void EnterDungeon()
        {
            Console.Clear();
            Console.WriteLine("■던전입장■");
            Console.WriteLine("던전에 도전하여 보상을 획득할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine($"현재 {dungeon.Now_Stage}층에 도전 중 입니다.");

            Console.WriteLine();
            Console.WriteLine("[상태]");
            Console.WriteLine($"LV. {character.Level}");
            Console.WriteLine($"Chad ({character.Name})");
            Console.WriteLine($"공격력 : {character.Total_Attack}");
            Console.WriteLine($"방어력 : {character.Total_Defence}");
            Console.WriteLine($"체  력 : {character.Health}");
            Console.WriteLine($"마  나 : {character.Mp}");
            Console.WriteLine($"속  도 : {character.Total_Speed}");
            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine("1. 던전 입장");
            Console.WriteLine("2. 던전 초기화");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int act = IsValidInput(2, 0);

            switch (act)
            {
                case 1:
                    dungeon.GoDungeon(character,portionlist);
                    break;
                case 2:
                    ResetDungeon();
                    break;
                case 0:
                    break;
            }
            
        }

        void ResetDungeon()
        {
            Console.Clear();
            Console.WriteLine("■던전초기화■");
            Console.WriteLine("던전을 초기화 합니다.");
            Console.WriteLine("초기화 할 경우, 던전을 1층부터 다시 도전해야 합니다. 정말로 초기화 하시겠습니까?");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("1. 예 2.아니오");
            Console.WriteLine();

            int choose = IsValidInput(2, 1);
            switch (choose)
            {
                case 1:
                    dungeon.ResetDungeon();
                    break;
                case 2:
                    EnterDungeon();
                    break;
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //3. 회복 아이템
        private void ViewPortion()
        {
            Console.Clear();
            Console.WriteLine("■회복■");
            Console.WriteLine($"포션을 사용하면 체력 또는 마나를 회복할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[포션 목록]");
            portionlist.PrintPortionList();
            Console.WriteLine();
            Console.WriteLine("1. 체력 회복하기");
            Console.WriteLine("2. 마나 회복하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(2, 0);
            switch (act)
            {
                case 0:
                    break;
                case 1:
                    if (portionlist.CheckPortion(PortionType.HP)) UsePortion(PortionType.HP);
                    else
                    {
                        Console.WriteLine("보유 중인 포션이 없습니다.");
                        Console.ReadKey();
                        ViewPortion();
                    }
                    break;
                case 2:
                    if (portionlist.CheckPortion(PortionType.MP)) UsePortion(PortionType.MP);
                    else
                    {
                        Console.WriteLine("보유 중인 포션이 없습니다.");
                        Console.ReadKey();
                        ViewPortion();
                    }
                    break;
            }
        }

        private void UsePortion(PortionType portionType)
        {
            Console.Clear ();
            Console.WriteLine("■회복 - 사용■");
            Console.Write($"포션을 사용하면 ");
            Console.Write((portionType == PortionType.HP) ? "체력" : "마나");
            Console.WriteLine("를 회복할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine((portionType == PortionType.HP) ? $"최대 체력 : {character.MaxHealth}" : $"최대 마나 : {character.MaxMp}");
            Console.WriteLine((portionType == PortionType.HP) ? $"현재 체력 : {character.Health}" : $"현재 마나 : {character.Mp}");
            Console.WriteLine();
            Console.WriteLine("[포션 목록]");
            int listlenght = portionlist.UsePortionList(portionType);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            int act = IsValidInput(listlenght, 0);

            if (act == 0)
            {
                ViewPortion();
                return;
            }
            else
            {
                Portion portion = portionlist.GetPortion(portionType, act);
                portionlist.UsePortion(character, portion);
                ViewPortion();
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
                inventory.SetPlayerSpec(character, selectitem);
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

    }
}
