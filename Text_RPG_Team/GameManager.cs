using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class GameManager
    {
        Player character = new Player();
        Dungeon dungeon = new Dungeon();

        public GameManager()
        {
            FirstScreen();
            character.Name = SetCharacter();
            SetJob();
            MainTown();

        }

        //---------------------------------------------------------------------------------------------------------------
        //게임 시작 초기 화면 함수
        void FirstScreen()
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
            Console.WriteLine(PadLeftForMixedText("Press Any Key to Play", 93));
            Console.WriteLine("=============================================================================================");
            Console.ReadKey();
        }

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
            Console.WriteLine("1.전사 2.마법사 3.도적");
            Console.Write(">>");
            int chooseJob = IsValidInput(3, 1);
            switch(chooseJob)
            {
                case 1:
                    character.Job = JOB.WARRIOR;
                    character.Health = 200;
                    character.Attack = 5;
                    character.Defense = 10;
                    setJob = "전사";
                    break;
                case 2:
                    character.Job = JOB.WIZARD;
                    character.Health = 100;
                    character.Attack = 10;
                    character.Defense = 5;
                    setJob = "마법사";
                    break;
                case 3:
                    character.Job = JOB.ROGUE;
                    character.Health = 150;
                    character.Attack = 8;
                    character.Defense = 8;
                    setJob = "도적";
                    break;
            }
            Console.WriteLine($"당신의 직업은 {setJob} 입니다. 확정하시겠습니까?");
            Console.WriteLine("1. 예 2.아니오");
            Console.Write(">>");
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
        private void MainTown()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine();

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 전투시작");

            Console.WriteLine();

            int act = IsValidInput(2, 1);

            switch (act)
            {
                case 1:
                    ViewState();
                    break;
                case 2:
                    EnterDungeon();
                    break;
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        private void ViewState()
        {
            character.ViewState();

            Console.WriteLine();

            int act = IsValidInput(0, 0);

            switch (act)
            {
                case 0:
                    MainTown();
                    break; ;
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
        //출력 정렬하는 함수
        private int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }
            return length;
        }

        private string PadLeftForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = (totalLength - currentLength) / 2;
            return str.PadLeft(str.Length + padding);
        }

        private string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = (totalLength - currentLength) / 2;
            return str.PadRight(str.Length + padding);
        }
    }
}
