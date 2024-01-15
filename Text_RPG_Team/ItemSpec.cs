using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    public enum SpecType
    {
        ATTACK, DEFEND, SPEED, CRIT_RATE, CRIT_DMG, EVASION
    }

    [Serializable]
    public class ItemSpec
    {

        Dictionary<SpecType, int> specMap;

        //아이템 스펙 생성
        public ItemSpec(Dictionary<SpecType, int> specMap)
        {
            if(specMap == null)
            {
                this.specMap = new Dictionary<SpecType, int>();
            }
            else
            {
                this.specMap = new Dictionary<SpecType, int>(specMap);
            }
        }

        //아이템 능력치 수치
        public int GetSpec(SpecType specType)
        {
            return specMap[specType];
        }

        //아이템 전체 능력치
        public Dictionary<SpecType, int> SpecMap
        {
            get { return specMap; }
        }

        //아이템 능력치 출력
        public void getSpecList()
        {
            foreach (KeyValuePair<SpecType, int> s in specMap)
            {
                switch (s.Key)
                {
                    case SpecType.ATTACK:
                        Console.Write($" 공격력 {(s.Value > 0 ? "+" : "")}{s.Value} ");
                        break;
                    case SpecType.DEFEND:
                        Console.Write($" 방어력 {(s.Value > 0 ? "+" : "")}{s.Value} ");
                        break;
                    case SpecType.SPEED:
                        Console.Write($" 속  도 {(s.Value > 0 ? "+" : "")}{s.Value} ");
                        break;
                    case SpecType.CRIT_RATE:
                        Console.Write($" 치명타율 {(s.Value > 0 ? "+" : "")}{s.Value}% ");
                        break;
                    case SpecType.CRIT_DMG:
                        Console.Write($" 치명타피해 {(s.Value > 0 ? "+" : "")}{s.Value}% ");
                        break;
                    case SpecType.EVASION:
                        Console.Write($" 회피율 {(s.Value > 0 ? "+" : "")}{s.Value}% ");
                        break;
                }
            }
            

        }

    }
}
