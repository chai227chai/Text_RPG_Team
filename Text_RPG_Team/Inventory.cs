using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class Inventory
    {
        private List<Item> inventoryList;

        private Dictionary<ItemType, Item> equipedTem;//장착된 아이템

        public Inventory()
        {
            this.inventoryList = new List<Item>();
            this.equipedTem = new Dictionary<ItemType, Item>();
        }

        //인벤토리에 아이템 추가
        public void addInventroy(Item item)
        {
            Item Inventory_item = new Item(item);
            inventoryList.Add(Inventory_item);
        }

        //인벤토리에 있는 아이템 장착
        public void AddEquipedTem(ItemType type, Item item)
        {
            if (equipedTem.ContainsKey(type) && equipedTem[type] != item)
            {
                equipedTem[type].SetEquip();
                equipedTem.Remove(type);

                equipedTem.Add(type, item);
                item.SetEquip();
            }
            else if (equipedTem.ContainsKey(type) && equipedTem[type] == item)
            {
                item.SetEquip();
                equipedTem.Remove(type);
            }
            else
            {
                equipedTem.Add(type, item);
                item.SetEquip();
            }
        }

        //인벤토리에 있는 아이템 하나 가져오기
        public Item GetItem(int n)
        {
            return this.inventoryList[n - 1];
        }

        //인벤토리에서 아이템 삭제
        public void RemoveItem(int n)
        {
            inventoryList.RemoveAt(n - 1);
        }

        //장착한 아이템의 공격력 가져오기
        public int ExAttack()
        {
            int exAttack = 0;
            foreach (Item item in inventoryList.FindAll(isequip => isequip.IsEquip == true && isequip.GetSpecType == SpecType.ATTACK))
            {
                exAttack += item.GetSpec;
            }

            return exAttack;
        }

        //장착한 아이템의 방어력 가져오기
        public int ExDefend()
        {
            int exDefend = 0;
            foreach (Item item in inventoryList.FindAll(isequip => isequip.IsEquip == true && isequip.GetSpecType == SpecType.DEFEND))
            {
                exDefend += item.GetSpec;
            }

            return exDefend;
        }

        //인벤토리 리스트 가져오기
        public List<Item> GetInventoryList
        {
            get { return inventoryList; }
        }
    }
}

