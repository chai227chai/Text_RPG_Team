﻿using System;
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
        int Attack { get; set; }
        int Defence { get; set; }
        bool IsDead { get; }

        CHAR_TAG Tag { get; }

        void TakeDamage(int damage);
    }
}
