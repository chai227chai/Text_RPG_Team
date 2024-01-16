using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    public class TextEdit
    {
        //---------------------------------------------------------------------------------------------------------------
        //출력 정렬하는 함수
        private static int GetPrintableLength(string str)
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

        public static string PadLeftForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = (totalLength - currentLength) / 2;
            return str.PadLeft(str.Length + padding);
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = (totalLength - currentLength);
            return str.PadRight(str.Length + padding);
        }

        //---------------------------------------------------------------------------------------------------------------
        //text 색 바꿔주는 함수
        public static void ChangeTextColorMagenta(string text) //화면 제목에 사용
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void ChangeTextColorDarkMagenta(string text) //아이템 선택 가능할 때 사용
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void ChangeTextColorCyan(string text) //아이템 선택 가능할 때 사용
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void ChangeTextColorYellow(string text) //노란색 텍스트 출력
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void HighlightsTextColorYellow(string s1, string s2, string s3 = "") //스탯 보여줄 때 사용
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }
    }
}
