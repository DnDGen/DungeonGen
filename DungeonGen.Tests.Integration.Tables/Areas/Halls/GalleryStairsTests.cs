﻿using DungeonGen.Domain.Tables;
using NUnit.Framework;

namespace DungeonGen.Tests.Integration.Tables.Areas.Halls
{
    [TestFixture]
    public class GalleryStairsTests : PercentileTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.GalleryStairs;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 75, ContentsConstants.GalleryStairs_End)]
        [TestCase(76, 100, ContentsConstants.GalleryStairs_Beginning)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
