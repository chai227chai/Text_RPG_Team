using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Text_RPG_Team
{
    internal class GameManager
    {
        Player character;
        Dungeon dungeon = new Dungeon();
        PortionList portionList = new PortionList();
        ItemList itemList = new ItemList();
        Store store = new Store();

        string name;

        public GameManager()
        {
            FirstScreen();
            while (character is null)
            {
                StartGame();
            }
            MainTown();
        }

        //---------------------------------------------------------------------------------------------------------------
        //게임 시작 초기 화면 함수
        private void FirstScreen()
        {       
            bool exit_loop = false;
            ConsoleColor[] colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta };
            int cnt = 0;

            while (!exit_loop)
            {
                Console.ForegroundColor = colors[cnt % colors.Length];
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
                TextEdit.ChangeTextColorCyan(TextEdit.PadLeftForMixedText("Press Any Key to Play", 93));
                Console.ForegroundColor = colors[cnt % colors.Length];
                Console.WriteLine("=============================================================================================");
                if (Console.KeyAvailable)
                {
                    Console.ReadKey();
                    Console.ResetColor();
                    exit_loop = true; 
                    break;
                }

                Thread.Sleep(500);
                Console.Clear();
                cnt++;
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //게임 시작
        private void StartGame()
        {
            Console.Clear();
            Console.WriteLine("...................................................................................................................");
            Console.WriteLine("..$$$$....$$$$.............$$$$$$..$$$$$$.......$$$$$...$$$$$$...$$$$...$$$$$$..$$..$$...$$$$......................");
            Console.WriteLine(".$$......$$..$$..............$$......$$.........$$..$$..$$......$$........$$....$$$.$$..$$.........................");
            Console.WriteLine("..$$$$...$$..$$..............$$......$$.........$$$$$...$$$$....$$.$$$....$$....$$.$$$...$$$$......................");
            Console.WriteLine(".....$$..$$..$$...$$.........$$......$$.........$$..$$..$$......$$..$$....$$....$$..$$......$$....$$....$$....$$...");
            Console.WriteLine("..$$$$....$$$$.....$.......$$$$$$....$$.........$$$$$...$$$$$$...$$$$...$$$$$$..$$..$$...$$$$.....$$....$$....$$...");
            Console.WriteLine("...................................................................................................................");
            Console.WriteLine();

            TextEdit.ChangeTextColorYellow("1. 새로 시작하기");
            TextEdit.ChangeTextColorCyan("2. 게임 불러오기");
            Console.WriteLine();

            int choose_job = IsValidInput(2, 1);
            switch (choose_job)
            {
                case 1:
                    name = SetCharacter();
                    SetJob();
                    break;
                case 2:
                    LoadGame();
                    break;
            }
            
        }

        //---------------------------------------------------------------------------------------------------------------
        public string SetCharacter()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">> ");
            return Console.ReadLine();
        }
        public void SetJob() //직업선택후 직업에 따른스탯변경
        {
            Console.Clear();
            Console.WriteLine("캐릭터의 직업을 선택해 주세요");
            Console.WriteLine();
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("전사  ");
            Console.ResetColor();
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("마법사  ");
            Console.ResetColor();
            Console.Write("3. ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("도적");
            Console.ResetColor();

            Console.WriteLine();
          
            int choose_job = IsValidInput(3, 1);
            switch(choose_job)
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
            Console.Write($"당신의 직업은 ");
            switch (character.GetJob)
            {
                case "전사":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "마법사":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "도적":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    break;
            }
            Console.Write($"{character.GetJob}");
            Console.ResetColor();
            Console.WriteLine(" 입니다. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("확정하시겠습니까?");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("1. 예 2. 아니오");
            Console.WriteLine();

            int choose = IsValidInput(2, 1);
            switch (choose)
            {
                case 1:
                    character.Gold = 1500;
                    character.UseSkill();
                    portionList.AddPortion(PortionType.HP, PortionValue.Small, 3);
                    portionList.AddPortion(PortionType.MP, PortionValue.Small, 3);
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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 상태보기");
            Console.WriteLine($"2. 전투시작  (현재 층 수 : {dungeon.Now_Stage}층)");
            Console.WriteLine("3. 회복 아이템");
            Console.WriteLine("4. 인벤토리");
            Console.WriteLine("5. 상점");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("6. 게임 세이브");
            Console.WriteLine("7. 게임 로드");

            Console.WriteLine();

            int act = IsValidInput(7, 1);

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
                case 6:
                    SaveGame();
                    MainTown();
                    break;
                case 7:
                    LoadGame();
                    MainTown();
                    break;
            }
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
                if (cnt == 0)
                {
                    Console.WriteLine("원하시는 행동을 입력해 주세요.");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("다시 입력해 주세요.");
                }
                Console.Write(">> ");
                result = int.TryParse(Console.ReadLine(), out key_input);

                cnt = 1;
            } while (result == false || IsValidInput(key_input, min, max) == false);

            return key_input;
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■상태보기■");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("LV : " + character.Level.ToString("00"));
            Console.Write($"{character.Name} ");
            Console.Write("( ");
            switch (character.GetJob)
            {
                case "전사":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "마법사":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "도적":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    break;
            }
            Console.Write($"{character.GetJob}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" )");
            Console.Write($"공격력 : {character.TotalAttack}");
            Console.WriteLine((character.PlusAttack != 0) ? $" ({(character.PlusAttack > 0 ? "+" : "")}{character.PlusAttack})" : "");
            Console.Write($"방어력 : {character.TotalDefence}");
            Console.WriteLine((character.PlusDefence != 0) ? $" ({(character.PlusDefence > 0 ? "+" : "")}{character.PlusDefence})" : "");
            Console.WriteLine($"체  력 : {character.Health}");
            Console.WriteLine($"마  나 : {character.Mp}");
            Console.Write($"속  도 : {character.TotalSpeed}");
            Console.WriteLine((character.PlusSpeed != 0) ? $" ({(character.PlusSpeed > 0 ? "+" : "")}{character.PlusSpeed})" : "");
            if (character.PlusCritRate != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"치명타율 : {character.TotalCritRate} ({(character.PlusCritRate > 0 ? "+" : "")}{character.PlusCritRate})");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (character.PlusCritDMG != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"치명타피해 : {character.TotalCritDMG.ToString("N2")} ({(character.PlusCritDMG > 0 ? "+" : "")}{character.PlusCritDMG})");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (character.PlusEvasion != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"회피율 : {character.TotalEvasion} ({(character.PlusEvasion > 0 ? "+" : "")}{character.PlusEvasion})");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine($"Gold : {character.Gold}G");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("■던전입장■");
            Console.ResetColor();
            Console.WriteLine("던전에 도전하여 보상을 획득할 수 있습니다.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            Console.WriteLine($"현재 {dungeon.Now_Stage}층에 도전 중 입니다.");

            Console.WriteLine();
            Console.WriteLine("[상태]");
            Console.WriteLine($"LV. {character.Level}");
            Console.WriteLine($"({character.Name}) ({character.GetJob})");
            Console.WriteLine($"공격력 : {character.TotalAttack}");
            Console.WriteLine($"방어력 : {character.TotalDefence}");
            Console.WriteLine($"체  력 : {character.Health}");
            Console.WriteLine($"마  나 : {character.Mp}");
            Console.WriteLine($"속  도 : {character.TotalSpeed}");
            Console.ResetColor();
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
                    dungeon.GoDungeon(character,portionList);
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("■던전초기화■");
            Console.ResetColor();
            Console.WriteLine("던전을 초기화 합니다.");
            Console.WriteLine("초기화 할 경우, 던전을 1층부터 다시 도전해야 합니다. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("정말 초기화 하시겠습니까?");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("■회복■");
            Console.ResetColor();
          
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"포션을 사용하면 체력 또는 마나를 회복할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[포션 목록]");
            portionList.PrintPortionList();
          
            Console.WriteLine();
            Console.ResetColor();
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
                    if (portionList.CheckPortion(PortionType.HP)) UsePortion(PortionType.HP);
                    else
                    {
                        Console.WriteLine("보유 중인 포션이 없습니다.");
                        Console.ReadKey();
                        ViewPortion();
                    }
                    break;
                case 2:
                    if (portionList.CheckPortion(PortionType.MP)) UsePortion(PortionType.MP);
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("■회복 - 사용■");
            Console.ResetColor();
            Console.Write($"포션을 사용하면 ");
            Console.Write((portionType == PortionType.HP) ? "체력" : "마나");
            Console.WriteLine("를 회복할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine((portionType == PortionType.HP) ? $"최대 체력 : {character.MaxHealth}" : $"최대 마나 : {character.MaxMp}");
            Console.WriteLine((portionType == PortionType.HP) ? $"현재 체력 : {character.Health}" : $"현재 마나 : {character.Mp}");
            Console.WriteLine();
            Console.WriteLine("[포션 목록]");
            int list_lenght = portionList.UsePortionList(portionType);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            int act = IsValidInput(list_lenght, 0);

            if (act == 0)
            {
                ViewPortion();
                return;
            }
            else
            {
                Portion portion = portionList.GetPortion(portionType, act);
                portionList.UsePortion(character, portion);
                ViewPortion();
            }
        }
      
        //---------------------------------------------------------------------------------------------------------------
        //4. 인벤토리
        private void ViewInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("■인벤토리■");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[아이템 목록]");
            try
            {
                itemList.PrintItemList(character.GetInventory.GetInventoryList);
            }
            catch (ArgumentOutOfRangeException ex) 
            {
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ResetColor();

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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("■인벤토리 - 장착 관리■");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[아이템 목록]");
            try
            {
                itemList.PrintItemList(character.GetInventory.GetInventoryList, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine();
            }
            Console.WriteLine("");
            Console.ResetColor();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            int act = IsValidInput(character.GetInventory.GetInventoryList.Count, 0);

            if (act == 0)
            {
                ViewInventory();
                return;
            }
            else
            {
                Item selectitem = character.GetInventory.GetItem(act);
                character.GetInventory.SetPlayerSpec(character, selectitem);
                InventoryManager();
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        //5. 상점
        private void ViewStore()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("■상점■");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻거나 가지고 있는 아이템을 팔 수 있는 상점입니다.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold}G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            itemList.PrintItemList(itemList.GetItemList, false ,true);
            Console.WriteLine();
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("■상점 - 아이템 구매■");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold}G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            itemList.PrintItemList(itemList.GetItemList, true, true);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(itemList.ItemNumber, 0);

            if (act == 0)
            {
                ViewStore();
                return;
            }
            else
            {
                Item solditem = store.BuyItem(act, itemList, character.Gold);
                if(solditem != null)
                {
                    character.Gold -= solditem.Price;
                    character.GetInventory.AddInventroy(solditem);
                    StoreBuy();
                }
                else
                {
                    ViewStore();
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            string root_dir = ".\\SaveData";
            if(Directory.Exists(root_dir) == false)
            {
                Directory.CreateDirectory(root_dir);
            }
            FileStream fs = new FileStream( root_dir + "\\savefile" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".save", FileMode.Create);
            SaveField dataSave = new SaveField();

            dataSave.character = this.character;
            dataSave.itemList = this.itemList;
            dataSave.portionList = this.portionList;
            dataSave.dungeon = this.dungeon;

            bf.Serialize(fs, dataSave);
            fs.Close();

        }

        void LoadGame()
        {
            string root_dir = ".\\SaveData";
            if (Directory.Exists(root_dir) == false)
            {
                Directory.CreateDirectory(root_dir);
            }

            string[] files = Directory.GetFiles(root_dir);
            Array.Sort(files);
            Array.Reverse(files);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("■게임 로드■");
            Console.ResetColor();
            Console.WriteLine("세이브 데이터를 선택하세요.");
            Console.WriteLine();

            int index = 1;
            foreach(string file in files)
            {
                Console.WriteLine(index + "." + file);
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            int act = IsValidInput(index - 1, 0);

            if (act == 0)
            {
                return;
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(files[act - 1], FileMode.Open);
                SaveField dataSave = new SaveField();

                dataSave = bf.Deserialize(fs) as SaveField;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("해당 캐릭터로 시작하시겠습니까?");
                Console.WriteLine();
                Console.ResetColor();
                Console.Write($"{dataSave.character.Name} ");
                Console.Write("( ");
                switch (dataSave.character.GetJob)
                {
                    case "전사":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case "마법사":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "도적":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    default:
                        break;
                }
                Console.Write($"{dataSave.character.GetJob}");
                Console.ResetColor();
                Console.Write(" )");
                Console.WriteLine();
                Console.WriteLine("LV : " + dataSave.character.Level.ToString("00"));
                Console.WriteLine("현재 도전 중인 층 : " + dataSave.dungeon.Now_Stage);
                Console.WriteLine();

                Console.WriteLine("1. 예 2. 아니오");
                Console.WriteLine();

                int act2 = IsValidInput(2, 1);

                switch (act2)
                {
                    case 1:
                        this.character = dataSave.character;
                        this.itemList = dataSave.itemList;
                        this.portionList = dataSave.portionList;
                        this.dungeon = dataSave.dungeon;
                        fs.Close();
                        break;
                    case 2:
                        fs.Close();
                        LoadGame();
                        break;
                }

            }

            return;
        }

    }
}
