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
<<<<<<< HEAD
        public enum PortionType
=======
        public int myportion =0;

        public Portion()
        {
        }

        //초기포션
        public void StartPortion(int n)
>>>>>>> main
        {
            HP, MP
        }

<<<<<<< HEAD
        private int amoportion;
        private int portioncount;
        private PortionType type;
        private Portion hpportion;
        private Portion mpportion;


        public int AmoPortion
=======
        //포션 생성
        public void GetPortion(int n)
>>>>>>> main
        {
            get { return amoportion; }
            set { amoportion = value; }
        }

        public int PortionCount
        {
            get { return portioncount; }
            set { portioncount = value; }
        }

        public PortionType Type 
        { 
            get { return type; }
            set { type = value; }
        }

        public Portion HpPortion
        {
            get { return hpportion; }
        }

        public Portion MpPortion
        {
            get { return mpportion; }
        }

        //생성자
        public Portion(PortionType type, int n)
        {
            this.type = type;
            if (type == PortionType.HP)
            {
                HpPortion.portioncount = n;
            }
            else if (type == PortionType.MP)
            {
                HpPortion.portioncount = n;
            }
        }

        //포션 갯수 추가, 수정 필요
        private void GetPortion(PortionType type, int n)
        {
            PortionCount += n;
            Type = type;
        }

        //HP 포션 사용
        public void UsePortion(Player player)
        {
            if( PortionCount <= 0) 
            {
                Console.WriteLine("포션이 부족합니다.");
            }
            else
            {
                if (player.MaxHealth - player.Health < 30)
                {
                    player.Health = player.MaxHealth;
                }
                else player.Health += 30;

                PortionCount--;
                Console.WriteLine("회복을 완료했습니다.");
            }
        }
    }
}
