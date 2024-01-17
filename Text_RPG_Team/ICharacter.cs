using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    public enum CHAR_TAG
    {
        PLAYER, MONSTER
    }

    internal interface ICharacter
    {
        string Name { get; set; }

        int Level { get; set; }
        int Health { get; set; }
        int MaxHealth { get; }
        int Attack { get; set; }
        int Defence { get; set; }
        int TotalDefence { get; }
        int Speed { get; set; }
        int CritRate { get; }
        int TotalCritRate { get; }
        float CritDMG { get; }
        float TotalCritDMG { get; }
        int Evasion { get; }
        int TotalEvasion { get; }

        bool IsDead { get; }

        CHAR_TAG Tag { get; }

        void TakeDamage(int damage);

        int RanSpeed();

        int RanAttack();
    }
}
