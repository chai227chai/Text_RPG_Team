﻿using System;
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
        //구매
        public Item BuyItem(int number, ItemList itemlist, int playergold)
        {
            Item item = itemlist.GetItem(number);
            while (true)
            {
                if (!item.GetSale())
                {
                    Console.WriteLine("이미 구매하신 아이템입니다.");
                }
                else
                {
                    if (playergold < item.Price)
                    {
                        Console.WriteLine("보유하신 골드가 부족합니다.");
                    }
                    else
                    {
                        item.SetSale();
                        break;
                    }
                }
                Console.WriteLine("다시 입력해 주세요.");
                Console.Write(">>");
                string select = Console.ReadLine();
                int Renumber;
                bool check = int.TryParse(select, out Renumber);
                if (check)
                {
                    if (Renumber == 0)
                    {
                        item = null;
                        break;
                    }
                    else
                    {
                        item = BuyItem(Renumber, itemlist, playergold);
                        break;
                    }
                }
                else
                {
                    BuyItem(number, itemlist, playergold);
                }
                break;
            }
            return item;
        }
    }
}
