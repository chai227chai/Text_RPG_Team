﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal interface ISkill
    {
        string Name { get; }
        float Coefficient { get; }
    }
}
