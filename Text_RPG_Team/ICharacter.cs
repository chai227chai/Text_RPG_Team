using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal interface ICharacter
    {
        string Name { get; set; }
        int Level { get; set; }
        int Health { get; set; }
        int Attack { get; set; }
        int Defense {  get; set; }
        bool IsDead { get; }

        void TakeDamage(int damage);
    }
}
