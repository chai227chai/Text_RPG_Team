using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    public enum SpecType
    {
        ATTACK, DEFEND
    }

    public class ItemSpec
    {
        private SpecType specType;//아이템 능력치 유형
        private int spec;//아이템 수치

        public ItemSpec(SpecType specType, int spec)
        {
            this.specType = specType;
            this.spec = spec;
        }

        //아이템 능력치 수치
        public int getSpec()
        {
            return spec;
        }

        //아이템 능력치 유형
        public SpecType getSpecType()
        {
            return this.specType;
        }

        //아이템 능력치 유형 출력
        public string getSpecName()
        {
            switch (this.specType)
            {
                case SpecType.ATTACK:
                    return "공격력 +" + spec.ToString();
                case SpecType.DEFEND:
                    return "방어력 +" + spec.ToString();
            }

            return "";
        }

    }
}
