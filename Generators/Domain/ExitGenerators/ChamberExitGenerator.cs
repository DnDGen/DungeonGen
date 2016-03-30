﻿using DungeonGen.Common;
using DungeonGen.Selectors;
using DungeonGen.Tables;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGen.Generators.Domain.ExitGenerators
{
    public class ChamberExitGenerator : ExitGenerator
    {
        private IAreaPercentileSelector areaPercentileSelector;
        private AreaGenerator hallGenerator;
        private AreaGenerator doorGenerator;
        private IPercentileSelector percentileSelector;

        public ChamberExitGenerator(IAreaPercentileSelector areaPercentileSelector, AreaGenerator hallGenerator, AreaGenerator doorGenerator, IPercentileSelector percentileSelector)
        {
            this.areaPercentileSelector = areaPercentileSelector;
            this.hallGenerator = hallGenerator;
            this.doorGenerator = doorGenerator;
            this.percentileSelector = percentileSelector;
        }

        public IEnumerable<Area> Generate(int dungeonLevel, int partyLevel, int length, int width)
        {
            var selectedExit = areaPercentileSelector.SelectFrom(TableNameConstants.ChamberExits);

            if (selectedExit.Length > 0 && length * width > selectedExit.Length)
                selectedExit.Width++;

            var exits = new List<Area>();

            while (selectedExit.Width-- > 0)
            {
                var exit = GetExit(dungeonLevel, partyLevel, selectedExit.Type);
                var additionalDescriptions = GetDescription(exit);
                exit.Descriptions = exit.Descriptions.Union(additionalDescriptions);

                exits.Add(exit);
            }

            return exits;
        }

        private Area GetExit(int dungeonLevel, int partyLevel, string type)
        {
            if (type == AreaTypeConstants.Door)
                return doorGenerator.Generate(dungeonLevel, partyLevel).Single();

            return hallGenerator.Generate(dungeonLevel, partyLevel).Single();
        }

        private IEnumerable<string> GetDescription(Area exit)
        {
            var location = percentileSelector.SelectFrom(TableNameConstants.ExitLocation);

            if (exit.Type == AreaTypeConstants.Door)
                return new[] { location };

            var direction = percentileSelector.SelectFrom(TableNameConstants.ExitDirection);
            return new[] { location, direction };
        }
    }
}
