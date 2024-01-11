using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
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

        //포션
        private Portion HpPortion = new Portion(PortionType.HP);
        private Portion MpPortion = new Portion(PortionType.MP);

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


        //인벤토리에 있는 아이템 장착 확인
        public void AddEquipedTem(ItemType type, Item item)
        {
            if (equipedTem.ContainsKey(type) && equipedTem[type] != item) //장착된게 있고, 현재 장착하고 있는 것과 다르면!
            {
                equipedTem[type].SetEquip(); //원래 끼고 있던 아이템 장착 해제
                equipedTem.Remove(type); //장착

                equipedTem.Add(type, item);
                item.SetEquip();
            }
            else if (equipedTem.ContainsKey(type) && equipedTem[type] == item) //장착이 되어있고, 현재 장착한 것과 같으면!
            {
                item.SetEquip(); //장착 해제
                equipedTem.Remove(type);
            }
            else //장착된게 없으면!
            {
                equipedTem.Add(type, item);
                item.SetEquip();
            }
        }

        //스탯 반영
        public void SetPlayerSpec(Player player, Item item)
        {
            AddEquipedTem(item.Type, item);

            player.PlusAttack = ExAttack();
            player.PlusDefence = ExDefend();
            player.PlusSpeed = ExSpeed();
        }
        

        //포션 가져오기
        public void GetPortion(Portion portion)
        {
            if(portion.Type == PortionType.HP)
            {
                HpPortion.Count = portion.Count;
            }
            else if(portion.Type == PortionType.MP)
            {
                MpPortion.Count = portion.Count;
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

        //장착한 아이템의 스피드 가져오기
        public int ExSpeed()
        {
            int exSpeed = 0;
            foreach (Item item in inventoryList.FindAll(isequip => isequip.IsEquip == true && isequip.GetSpecType == SpecType.SPEED))
            {
                exSpeed += item.GetSpec;
            }

            return exSpeed;
        }

        //인벤토리 리스트 가져오기
        public List<Item> GetInventoryList
        {
            get { return inventoryList; }
        }
    }
}

