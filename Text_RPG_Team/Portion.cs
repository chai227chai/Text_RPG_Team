using System;
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
            portioncount = 0;
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
            int pre_hp = 0;
            int pre_mp = 0;
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
                else
                {
                    pre_hp = player.Health;
                    player.Health = (player.MaxHealth - player.Health < 30) ? player.MaxHealth : player.Health + 30;
                    Count--;
                }
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
                    pre_mp = player.Mp;
                    player.Mp = (player.MaxMp - player.Mp < 30) ? player.MaxMp : player.Mp + 30;
                    Count--;
                }
            }
            Console.WriteLine("회복을 완료했습니다.");
            Console.Write((pre_hp != 0) ? $"{pre_hp} -> {player.Health}\n" : "");
            Console.Write((pre_mp != 0) ? $"{pre_mp} -> {player.Mp}\n" : "");
        }
    }
}
