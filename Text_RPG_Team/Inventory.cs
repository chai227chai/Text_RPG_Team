using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            int spec = 0;
            if (equipedTem.ContainsKey(type) && equipedTem[type] != item) //장착된게 있고, 현재 장착하고 있는 것과 다르면!
            {
                equipedTem[type].SetEquip(); //원래 끼고 있던 아이템 장착 해제
                spec = CalculSpec(item, equipedTem[type]);

                equipedTem.Remove(type); //없앰

                equipedTem.Add(type, item); //선택한거 추가
                item.SetEquip(); //장착
            }
            else if (equipedTem.ContainsKey(type) && equipedTem[type] == item) //장착이 되어있고, 현재 장착한 것과 같으면!
            {
                spec = CalculSpec(item, equipedTem[type]);
                item.SetEquip(); //장착 해제
                equipedTem.Remove(type); //없앰
            }
            else //장착된게 없으면!
            {
                spec = CalculSpec(item);
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

        //스펙 계산, item1은 착용할 것 item2는 착용하고 있는 것
        private int CalculSpec(Item item1, Item? item2 = null)
        {
            if (item2 == null)
            {
                return item1.GetSpec;
            }
            else
            {
                return item1.GetSpec - item2.GetSpec;
            }
        }

        //player한테 스펙 계산한거 넘겨주기
        public void SetSpec(ItemType type, int spec)
        {

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

