﻿using DnDGen.Core.Selectors.Percentiles;
using DungeonGen.Domain.Selectors;
using DungeonGen.Domain.Tables;
using EncounterGen.Generators;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGen.Domain.Generators.AreaGenerators
{
    internal class HallGenerator : AreaGenerator
    {
        public string AreaType
        {
            get { return AreaTypeConstants.Hall; }
        }

        private readonly IAreaPercentileSelector areaPercentileSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly Dice dice;

        public HallGenerator(IAreaPercentileSelector areaPercentileSelector, IPercentileSelector percentileSelector, Dice dice)
        {
            this.areaPercentileSelector = areaPercentileSelector;
            this.percentileSelector = percentileSelector;
            this.dice = dice;
        }

        public IEnumerable<Area> Generate(int dungeonLevel, EncounterSpecifications environment)
        {
            var hall = areaPercentileSelector.SelectFrom(TableNameConstants.Halls);

            if (hall.Type != AreaTypeConstants.Special)
                return new[] { hall };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            hall = areaPercentileSelector.SelectFrom(tableName);

            if (hall.Contents.Miscellaneous.Contains(ContentsConstants.Chasm))
            {
                hall = AddContents(hall, TableNameConstants.ChasmCrossing);
            }

            if (hall.Contents.Miscellaneous.Contains(ContentsConstants.River))
            {
                hall = AddContents(hall, TableNameConstants.RiverCrossing);
            }

            if (hall.Contents.Miscellaneous.Contains(ContentsConstants.Stream))
            {
                hall = AddContents(hall, TableNameConstants.StreamCrossing);
            }

            if (hall.Contents.Miscellaneous.Contains(ContentsConstants.Gallery))
            {
                hall = AddContents(hall, TableNameConstants.GalleryStairs);
            }

            if (hall.Contents.Miscellaneous.Contains(ContentsConstants.GalleryStairs_Beginning))
            {
                hall = AddContents(hall, TableNameConstants.AdditionalGalleryStairs);
            }

            return new[] { hall };
        }

        private Area AddContents(Area area, string tableName)
        {
            var additionalContents = percentileSelector.SelectFrom(tableName);

            if (string.IsNullOrEmpty(additionalContents))
                return area;

            if (dice.ContainsRoll(additionalContents))
                additionalContents = dice.ReplaceExpressionWithTotal(additionalContents);

            area.Contents.Miscellaneous = area.Contents.Miscellaneous.Union(new[] { additionalContents });

            return area;
        }
    }
}
