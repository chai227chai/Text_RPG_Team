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

    public enum PortionValue
    {
        Small = 30,
        Medium = 50, 
        Big = 100,
    }

    [Serializable]
    internal class Portion
    {
        private PortionType portionType;
        private PortionValue portionValue;

        private int portion_count;

        //생성자
        public Portion(PortionType type, PortionValue value)
        {
            portionType = type;
            portionValue = value;
            portion_count = 0;
        }

        //----------------------------------------------------------------------------------------------
        //변수 반환 함수

        public PortionType Type 
        {
            get { return portionType; }
            set { portionType = value; }
        }

        public int Count
        {
            get { return portion_count; }
            set { portion_count = value; }
        }

        public PortionValue Value
        {
            get { return portionValue; }
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

        //포션 회복량 가져오기
        public int GetValue()
        {
            return (int)Value;
        }
    }
}
