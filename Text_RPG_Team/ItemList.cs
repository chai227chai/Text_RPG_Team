using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class ItemList
    {
        private List<Item> itemList;

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
            AddItem("0", ItemType.ARMOR, "수련자 갑옷", "수련에 도움을 주는 갑옷입니다. ", 1000, new ItemSpec(SpecType.DEFEND, 5));
            AddItem("1", ItemType.ARMOR, "무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다. ", 2000, new ItemSpec(SpecType.DEFEND, 9));
            AddItem("2", ItemType.ARMOR, "스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, new ItemSpec(SpecType.DEFEND, 15));
            AddItem("3", ItemType.WEAPON, "낡은 검", "쉽게 볼 수 있는 낡은 검 입니다. ", 600, new ItemSpec(SpecType.ATTACK, 2));
            AddItem("4", ItemType.WEAPON, "청동 도끼", "어디선가 사용됐던거 같은 도끼입니다. ", 1500, new ItemSpec(SpecType.ATTACK, 5));
            AddItem("5", ItemType.WEAPON, "스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다. ", 3000, new ItemSpec(SpecType.ATTACK, 7));
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

        public void PrintItemList(bool checknumber = false)
        {
            if (checknumber)
            {
                int i = 1;
                foreach (Item item in itemList)
                {
                    Console.WriteLine($"{i}. {item.Name} | {item.GetSpec} | {item.Price} | {item.Detail}");
                    i++;
                }
            }
            else
            {
                foreach (Item item in itemList)
                {
                    Console.WriteLine($"{item.Name} | {item.GetSpec} | {item.Price} | {item.Detail}");
                }
            }
        }
    }
}
