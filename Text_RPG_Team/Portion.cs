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
        private int hpportion;
        private int mpportion;

        //생성자
        public Portion(PortionType type)
        {
            portiontype = type;
            if (portiontype == PortionType.HP)
            {
                hpportion = 0;
            }
            else if(portiontype == PortionType.MP)
            { 
                mpportion = 0; 
            }
        }

        //----------------------------------------------------------------------------------------------
        //변수 반환 함수

        public PortionType Type 
        {
            get { return portiontype; }
            set { portiontype = value; }
        }

        public int HpCount
        { 
            get { return hpportion; } 
            set { hpportion = value; }
        }

        public int MpCount
        {
            get { return mpportion; }
            set { mpportion = value; }
        }

        //----------------------------------------------------------------------------------------------
        //변수 조작 함수

        //포션 갯수 추가
        public void SetHpPortion(int n)
        {
            HpCount += n;
        }
        public void SetMpPortion(int n)
        {
            MpCount += n;
        }

        //포션 갯수 가져오기
        public int GetPortion()
        {
            return HpCount;
        }
        public int GetMpPortion()
        {
            return MpCount;
        }

        //포션 사용
        public void UseHpPortion(Player player)
        {
            if (HpCount <= 0)
            {
                Console.WriteLine("포션이 부족합니다.");
            }
            else
            {
                if(player.MaxHealth == player.Health)
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

                HpCount--;
                Console.WriteLine("회복을 완료했습니다.");
            }
        }

        public void UseMpPortion(Player player)
        {
            if (player.MaxMp == player.Mp)
            {
                Console.WriteLine();

                Console.WriteLine("최대입니다.");
                Console.WriteLine("처음 화면으로 돌아갑니다.");
                Thread.Sleep(2000);
                return;
            }
            else if (MpCount <= 0)
            {
                Console.WriteLine("포션이 부족합니다.");
            }
            else
            {
                if (player.MaxMp - player.Mp < 30)
                {
                    player.Mp = player.MaxMp;
                }
                else player.Mp += 30;

                HpCount--;
                Console.WriteLine("회복을 완료했습니다.");
            }
        }
    }
}
