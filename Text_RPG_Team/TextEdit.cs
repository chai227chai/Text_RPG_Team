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

        //---------------------------------------------------------------------------------------------------------------
        //글자 색 변경
        public void ChangeTextColorMagenta(string text) //화면 제목에 사용
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor(); //컬러 리셋을 해주지 않으면 아래에 나올 텍스트들도 마젠타 컬러로 나오게됨.
        }

        public void ChangeTextColorDarkMagenta(int idx) //아이템 선택 가능할 때 사용
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(idx);
            Console.Write(". ");
            Console.ResetColor();
        }

        public void HighlightsTextColorYellow(string s1, string s2, string s3 = "") //스탯 보여줄 때 사용
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }
    }
}
