﻿using DungeonGen.Tables;
using NUnit.Framework;

namespace DungeonGen.Tests.Unit.Tables
{
    [TestFixture]
    public class TableNameConstantsTests
    {
        [TestCase(TableNameConstants.DungeonAreaFromHall, "DungeonAreaFromHall")]
        public void Constant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
