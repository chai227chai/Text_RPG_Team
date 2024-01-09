using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class TextEdit
    {
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

        public string PadLeftForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = (totalLength - currentLength) / 2;
            return str.PadLeft(str.Length + padding);
        }

        public string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = (totalLength - currentLength) / 2;
            return str.PadRight(str.Length + padding);
        }
    }
}
