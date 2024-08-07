﻿using DnDGen.DungeonGen.Models;
using DnDGen.DungeonGen.Tables;
using NUnit.Framework;

namespace DnDGen.DungeonGen.Tests.Integration.Tables.Areas.Rooms
{
    [TestFixture]
    public class SpecialAreaShapesTests : PercentileTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.SpecialAreaShapes;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 25, DescriptionConstants.Circular)]
        [TestCase(26, 40, "Triangular")]
        [TestCase(41, 55, "Trapezoidal")]
        [TestCase(56, 65, "Odd shape (DM's choice)")]
        [TestCase(66, 75, "Ovular")]
        [TestCase(76, 85, "Hexagonal")]
        [TestCase(86, 95, "Octagonal")]
        [TestCase(96, 100, AreaTypeConstants.Cave)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
