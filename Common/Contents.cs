﻿using EncounterGen.Common;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGen.Common
{
    public class Contents
    {
        public IEnumerable<Encounter> Encounters { get; set; }
        public IEnumerable<ContainedTreasure> Treasures { get; set; }
        public IEnumerable<string> Miscellaneous { get; set; }
        public IEnumerable<Trap> Traps { get; set; }
        public Pool Pool { get; set; }

        public bool IsEmpty
        {
            get
            {
                return !Encounters.Any() && !Miscellaneous.Any() && !Traps.Any() && !Treasures.Any() && Pool == null;
            }
        }

        public Contents()
        {
            Treasures = Enumerable.Empty<ContainedTreasure>();
            Encounters = Enumerable.Empty<Encounter>();
            Miscellaneous = Enumerable.Empty<string>();
            Traps = Enumerable.Empty<Trap>();
        }
    }
}
