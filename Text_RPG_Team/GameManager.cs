using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class GameManager
    {
        Player player = new Player();
        Dungeon dungeon = new Dungeon();

        public GameManager()
        {
            MainTown();

        }


        //---------------------------------------------------------------------------------------------------------------
        //게임 시작 초기 화면 함수
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
                    break;
                case 2:
                    EnterDungeon();
                    break;
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        private void EnterDungeon()
        {
            dungeon.GoDungeon(player);

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
    }
}
