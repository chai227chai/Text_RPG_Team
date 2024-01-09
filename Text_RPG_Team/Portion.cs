using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Text_RPG_Team
{
    internal class Portion
    {
        public int myportion;

        public Portion(int n)
        {
            myportion = n;
        }

        //포션 생성
        private void GetPortion(int n)
        {
            myportion += n;
        }

        //포션 사용
        public void UsePortion(Player player)
        {
            if(myportion <= 0) 
            {
                Console.WriteLine("포션이 부족합니다.");
            }
            else
            {
                if (player.Health > 70)
                {
                    player.Health = 100;
                }
                else player.Health += 30;

                myportion--;
                Console.WriteLine("회복을 완료했습니다.");
            }
        }
    }
}
