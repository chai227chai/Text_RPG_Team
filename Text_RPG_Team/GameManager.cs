using System;
using System.Collections;
using System.Collections.Generic;
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
        Portion portion = new Portion(3);
        Store store = new Store();
        ItemList itemlist = new ItemList();

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
                    character.Health = 200;
                    character.Attack = 5;
                    character.Defence = 10;
                    setJob = character.GetJob;
                    break;
                case 2:
                    character.Job = JOB.WIZARD;
                    character.Health = 100;
                    character.Attack = 10;
                    character.Defence = 5;
                    setJob = character.GetJob;
                    break;
                case 3:
                    character.Job = JOB.ROGUE;
                    character.Health = 150;
                    character.Attack = 8;
                    character.Defence = 8;
                    setJob = character.GetJob;
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
                    break;
                case 2:
                    SetJob();
                    break;
            }

        }

        //---------------------------------------------------------------------------------------------------------------
        private void MainTown()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine();

            Console.WriteLine("1. 상태보기");
            Console.WriteLine($"2. 전투시작  (현재 층 수 : {dungeon.Now_Stage}층)");
            Console.WriteLine("3. 회복 아이템");
            Console.WriteLine("4. 상점");

            Console.WriteLine();

            int act = IsValidInput(4, 1);

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
                    store.getPlayer(character);
                    store.ViewStore();
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
        private void ViewPlayer()
        {
            Console.Clear();
            Console.WriteLine("■상태보기■");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("LV : " + character.Level.ToString("00"));
            Console.WriteLine($"{character.Name} ( {character.GetJob} )");
            Console.WriteLine($"공격력 : {character.Attack}");
            Console.WriteLine($"방어력 : {character.Defence}");
            Console.WriteLine($"체  력 : {character.Health}");
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
        private void ViewPortion()
        {
            Console.Clear();
            Console.WriteLine("■회복■");
            Console.WriteLine($"포션을 사용하면 체력을 30 회복할 수 있습니다. (남은 포션 : {portion.myportion})");
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
                    portion.UsePortion(character);
                    break;
            }
        }
    }
}
