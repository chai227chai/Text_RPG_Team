﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Text_RPG_Team
{
    public enum PortionType
    {
        HP, MP
    }

    internal class Portion
    {
        private PortionType portiontype;
        private int portioncount;

        //생성자
        public Portion(PortionType type)
        {
            portiontype = type;
        }

        //----------------------------------------------------------------------------------------------
        //변수 반환 함수

        public PortionType Type 
        {
            get { return portiontype; }
            set { portiontype = value; }
        }

        public int Count
        {
            get { return portioncount; }
            set { portioncount = value; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수

        //포션 갯수 추가
        public void SetPortion(int n)
        {
            Count = n;
        }

        public void AddPortion(int n)
        {
            Count += n;
        }

        //포션 갯수 가져오기
        public int GetCount()
        {
            return Count;
        }

        //포션 사용
        public void UsePortion(Player player)
        {
            if (Count <= 0)
            {
                Console.WriteLine("포션이 부족합니다.");
                Console.WriteLine("처음 화면으로 돌아갑니다.");
                Thread.Sleep(2000);
                return;
            }
            else if (Type == PortionType.HP)
            {
                if (player.MaxHealth == player.Health)
                {
                    Console.WriteLine();

                    Console.WriteLine("최대 체력입니다.");
                    Console.WriteLine("처음 화면으로 돌아갑니다.");
                    Thread.Sleep(2000);
                    return;
                }
                else if (player.MaxHealth - player.Health < 30)
                {
                    player.Health = player.MaxHealth;
                }
                else player.Health += 30;

                Count--;
            }
            else if (Type == PortionType.MP)
            {
                if (player.MaxMp == player.Mp)
                {
                    Console.WriteLine();

                    Console.WriteLine("최대 마나입니다.");
                    Console.WriteLine("처음 화면으로 돌아갑니다.");
                    Thread.Sleep(2000);
                    return;
                }
                else
                {
                    if (player.MaxMp - player.Mp < 30)
                    {
                        player.Mp = player.MaxMp;
                    }
                    else player.Mp += 30;

                    Count--;
                }
            }
            Console.WriteLine("회복을 완료했습니다.");
            Thread.Sleep(2000);
        }
        public void BattlePortion(Player player)
        {
            if (Count <= 0)
            {
                Console.WriteLine("포션이 부족합니다.");
                Thread.Sleep(2000);
                return;
            }
            else if (Type == PortionType.HP)
            {
                if (player.MaxHealth == player.Health)
                {
                    Console.WriteLine();

                    Console.WriteLine("최대 체력입니다.");
                    Thread.Sleep(2000);
                    return;
                }
                else if (player.MaxHealth - player.Health < 30)
                {
                    player.Health = player.MaxHealth;
                }
                else player.Health += 30;

                Count--;
            }
            else if (Type == PortionType.MP)
            {
                if (player.MaxMp == player.Mp)
                {
                    Console.WriteLine();

                    Console.WriteLine("최대 마나입니다.");
                    Thread.Sleep(2000);
                    return;
                }
                else
                {
                    if (player.MaxMp - player.Mp < 30)
                    {
                        player.Mp = player.MaxMp;
                    }
                    else player.Mp += 30;

                    Count--;
                }
            }
            Console.WriteLine("회복을 완료했습니다.");
            Thread.Sleep(2000);
        }
    }
}
