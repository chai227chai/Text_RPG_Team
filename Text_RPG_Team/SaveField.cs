using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    [Serializable]
    internal class SaveField
    {
        public Player character;
        public ItemList itemList;
        public PortionList portionlist;
        public Dungeon dungeon;
    }
}
