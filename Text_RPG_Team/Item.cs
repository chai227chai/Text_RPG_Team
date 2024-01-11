using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    public enum ItemType
    {
        WEAPON, ARMOR
    }

    internal class Item
    {
        private string name;
        private string detail;//아이템 설명
        private string number;//아이템 시리얼 넘버

        private int price;

        private ItemSpec spec;
        private ItemType itemType;

        private bool isSale = true;
        private bool isEquiped = false;

        public Item(string number, ItemType itemType, string name, string detail, int price, ItemSpec spec)
        {
            this.number = number;
            this.itemType = itemType;
            this.name = name;
            this.detail = detail;
            this.price = price;
            this.spec = spec;
        }

        public Item(Item item)
        {
            this.number = item.number;
            this.itemType = item.itemType;
            this.name = item.name;
            this.detail = item.detail;
            this.price = item.price;
            this.spec = item.spec;
        }

        //----------------------------------------------------------------------------------------------
        //변수 반환 함수

        //아이템 시리얼 넘버
        public string Number
        {
            get { return this.number; }
        }

        //아이템 이름
        public string Name
        {
            get { return this.name; }
        }

        //아이템 설명
        public string Detail
        {
            get { return this.detail; }
        }

        //아이템 가격
        public int Price
        {
            get { return this.price; }
        }

        //아이템 가격 출력
        public string SalePrice
        {
            get
            {
                if (isSale)
                {
                    string _saleprice = price.ToString() + " G";
                    return _saleprice;
                }
                else
                {
                    return "구매완료";
                }
            }
        }

        //아이템 판매 여부
        public bool GetSale()
        {
            return isSale;
        }

        //아이템 타입
        public ItemType Type
        {
            get { return this.itemType; }
        }

        //아이템 타입 출력
        public string GetType
        {
            get
            {
                switch (this.itemType)
                {
                    case ItemType.ARMOR:
                        return "갑옷";
                    case ItemType.WEAPON:
                        return "무기";
                }
                return "";
            }
        }

        //아이템 능력치 종류
        //public SpecType GetSpecType
        //{
        //    get { return spec.getSpecType(); }
        //}

        public ItemSpec Spec
        {
            get { return spec; }
        }

        //아이템 능력치 수치
        public int GetSpec(SpecType specType)
        {
            return spec.GetSpec(specType);
        }

        //아이템 능력치 종류 출력
        public void GetSpecName()
        {
            spec.getSpecList();
        }

        //아이템 장착 여부
        public bool IsEquip
        {
            get { return isEquiped; }
            set { isEquiped = value; }
        }

        //아이템 장착 여부 출력
        public string NowEquip
        {
            get
            {
                if (isEquiped)
                {
                    return "[E]";
                }
                else
                {
                    return "";
                }
            }
        }

        //----------------------------------------------------------------------------------------------

        //아이템 구매 시
        public void SetSale()
        {
            if (this.isSale)
            {
                this.isSale = false;
            }
            else
            {
                this.isSale = true;
            }
        }

        //아이템 장착 시
        public void SetEquip()
        {
            if (isEquiped)
            {
                isEquiped = false;
            }
            else
            {
                isEquiped = true;
            }
        }
    }
}
