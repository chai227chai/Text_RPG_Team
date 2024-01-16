using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    [Serializable]
    internal class ItemList
    {
        private List<Item> itemList;

        public int ItemNumber
        {
            get
            {
                return itemList.Count;
            }
        }

        public ItemList()
        {
            this.itemList = new List<Item>();
            Initialize();
        }

        //아이템 추가
        public void AddItem(string number, ItemType itemType, string name, string detail, int price, ItemSpec spec)
        {
            Item item = new Item(number, itemType, name, detail, price, spec);
            itemList.Add(item);
        }

        //여기에 새로운 아이템을 추가하시면 됩니다.
        public void Initialize()
        {

            AddItem("0", ItemType.ARMOR, "수련자 갑옷", "수련에 도움을 주는 갑옷입니다. ", 1000, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.DEFEND, 5 } }));

            AddItem("1", ItemType.ARMOR, "무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다. ", 2000, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.DEFEND, 9 } }));

            AddItem("2", ItemType.ARMOR, "스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.DEFEND, 9 } }));

            AddItem("9", ItemType.ARMOR, "바위 갑옷", "전설의 바위 기사가 사용했다고 알려진 무거운 갑옷입니다. ", 3000, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.DEFEND, 15 }, { SpecType.SPEED, -2 }, {SpecType.EVASION, -5 } }));

            AddItem("10", ItemType.ARMOR, "나노 강화 슈트", "신체 능력을 비약적으로 증가시키는 슈트입니다. ", 3500, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.DEFEND, 2 }, { SpecType.ATTACK, 5 }, { SpecType.CRIT_RATE, 10 }, {SpecType.CRIT_DMG, 5 } }));

            AddItem("11", ItemType.ARMOR, "산데비스탄", "기초적인 임플란트입니다. ", 3500, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.SPEED, 2 }, { SpecType.ATTACK, 5 }, {SpecType.EVASION, 20 } }));

            AddItem("3", ItemType.WEAPON, "낡은 검", "쉽게 볼 수 있는 낡은 검 입니다. ", 600, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.ATTACK, 2 } }));

            AddItem("4", ItemType.WEAPON, "청동 도끼", "어디선가 사용됐던거 같은 도끼입니다. ", 1500, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.ATTACK, 5 } }));

            AddItem("5", ItemType.WEAPON, "스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다. ", 3000, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.ATTACK, 7 } }));

            AddItem("6", ItemType.WEAPON, "강철 톤파", "공방 양면으로 우수한 무기입니다. ", 1500, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.ATTACK, 3 }, { SpecType.DEFEND, 5 }, {SpecType.EVASION, -3 } }));

            AddItem("12", ItemType.WEAPON, "저주받은 검", "사용자들을 죽음으로 몰고 갔다는 불길한 검입니다. ", 2500, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.ATTACK, 10 }, { SpecType.DEFEND, -10 }, { SpecType.CRIT_DMG, 25 } }));

            AddItem("7", ItemType.SHOES, "운동화", "평범한 신발입니다. ", 800, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.SPEED, 1 } }));

            AddItem("8", ItemType.SHOES, "블레이드 부츠", "칼날이 달려 날카로운 신발입니다. ", 1200, new ItemSpec(new Dictionary<SpecType, int>() { { SpecType.SPEED, 1 }, { SpecType.ATTACK, 2 } }));
        }

        //아이템 리스트에서 아이템 하나 가져오기
        public Item GetItem(int n)
        {
            return this.itemList[n - 1];
        }

        //아이템 리스트 가져오기
        public List<Item> GetItemList
        {
            get{ return itemList; }
        }


        //아이템 리스트 보여주기
        public void PrintItemList(List<Item> itemlist, bool checknumber = false, bool checkgold = false)
        {
            for (int i = 0; i < itemlist.Count; i++)
            {
                Console.Write("- ");
                if (checknumber)
                {
                    int n = i + 1;
                    Console.Write($"{n}. ");
                }
                Console.Write($"{itemlist[i].NowEquip}{itemlist[i].Name}");
                Console.Write(" | ");
                Console.Write($"{itemlist[i].GetType}");
                Console.Write(" | ");
                itemlist[i].GetSpecName();
                Console.Write(" | ");
                if (checkgold)
                {
                    Console.Write($"{itemlist[i].SalePrice}");
                    Console.Write(" | ");
                }
                Console.WriteLine(itemlist[i].Detail);
            }
        }
    }
}
