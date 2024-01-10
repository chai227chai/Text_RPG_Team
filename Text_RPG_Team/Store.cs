using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class Store
    {
        ItemList itemlist = new ItemList();
        Player character;
        public void getPlayer(Player player)
        {
            character = player;
        }

        List<Item> inventoryitem = new List<Item>();
        public List<Item> InventoryItem
        {
            get { return inventoryitem; }
            set { inventoryitem = value; }
        }

        //ViewStore()
        public void ViewStore()
        {
            Console.Clear();
            Console.WriteLine("■상점■");
            Console.WriteLine("필요한 아이템을 얻거나 가지고 있는 아이템을 팔 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold}G");//character.Gold
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            itemlist.PrintItemList();
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
            itemlist.PrintItemList(true);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int act = IsValidInput(itemlist.itemnumber, 0);

            if (act == 0)
            {
                return;
            }
            else
            {
                int number = act;
                Item solditem = itemlist.GetItem(number);
                solditem.SetSale();

                inventoryitem.Add(solditem);

                character.Gold -= solditem.Price;
                StoreBuy();
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
    }
}
