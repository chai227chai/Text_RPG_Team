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
    [Serializable]
    internal class PortionList
    {
        List<Portion> portionList = new List<Portion>();

        //포션 추가
        public void AddPortion(PortionType type, PortionValue value, int count)
        {
            Portion portion = new Portion(type, value);
            portion.Count = count;

            SumPortion(portion);
        }

        //같은 종류 포션 개수 합치기
        private void SumPortion(Portion portion)
        {
            for (int i = 0; i < portionList.Count; i++)
            {
                if (portionList[i].Type == portion.Type && portionList[i].Value == portion.Value)
                {
                    portionList[i].SetPortion(portion.Count);
                    return;
                }
            }
            portionList.Add(portion);
        }

        //포션 리스트 가져오기
        public List<Portion> GetPortionList() 
        {  
            return portionList; 
        }

        //포션 리스트 출력
        public void PrintPortionList()
        {
            portionList.Sort((portion1, portion2) => portion1.Type == portion2.Type ? portion1.Value.CompareTo(portion2.Value) : portion1.Type.CompareTo(portion2.Type));
            for (int i = 0; i < portionList.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine($"{portionList[i].Type} 포션 | {(int)portionList[i].Value} 회복 | {portionList[i].Count}개");
            }
        }

        public int UsePortionList(PortionType type)
        {
            int number = 1;
            for (int i = 0; i < portionList.Count; i++)
            {
                if (portionList[i].Type == type)
                {
                    Console.WriteLine($"- {number}. {portionList[i].Type} 포션 | {(int)portionList[i].Value} 회복 | {portionList[i].Count}개");
                    number++;
                }
                else if (portionList[i].Type == type)
                {
                    Console.WriteLine($"- {number}. {portionList[i].Type} 포션 | {(int)portionList[i].Value} 회복 | {portionList[i].Count}개");
                    number++;
                }
            }
            return number;
        }

        //포션 유무 확인
        public bool CheckPortion(PortionType type)
        {
            for (int i = 0; i < portionList.Count; i++)
            {
                if (portionList[i].Type == type)
                {
                    return true;
                }
            }
            return false;
        }

        //포션 가져오기
        public Portion GetPortion(PortionType type, int n)
        {
            int idx = 0;
            for (int i = 0; i < portionList.Count; i++)
            {
                if (portionList[i].Type != type)
                {
                    idx++;
                }
                else break;
            }
            return portionList[idx + n -1];
        }

        //포션 삭제하기
        private void RemovePortion(Portion portion)
        {
            portionList.Remove(portion);
        }

        //포션 사용하기
        public void UsePortion(Player player, Portion portion)
        {
            int pre;
            int value = (int)portion.Value;
            Console.WriteLine("회복을 완료했습니다.");
            switch (portion.Type)
            {
                case PortionType.HP:
                    pre = player.Health;
                    player.Health = (player.MaxHealth - player.Health < value) ? player.MaxHealth : player.Health + value;
                    Console.WriteLine($"HP : {pre} -> {player.Health}");
                    portion.Count--;
                    break;
                case PortionType.MP:
                    pre = player.Mp;
                    player.Mp = (player.MaxMp - player.Mp < value) ? player.MaxMp : player.Mp + value;
                    Console.WriteLine($"MP : {pre} -> {player.Mp}");
                    portion.Count--;
                    break;
            }
            if (portion.Count == 0) RemovePortion(portion);
            Console.ReadKey();
        }
    }
}
