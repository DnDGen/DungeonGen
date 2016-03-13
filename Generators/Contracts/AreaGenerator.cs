﻿using DungeonGen.Common;
using System.Collections.Generic;

namespace DungeonGen.Generators
{
    public interface AreaGenerator
    {
        IEnumerable<Area> Generate(int level);
    }
}