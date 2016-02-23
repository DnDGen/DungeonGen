﻿using DungeonGen.Common;
using NUnit.Framework;

namespace DungeonGen.Tests.Unit.Common
{
    [TestFixture]
    public class AreaTypeConstantsTests
    {
        [TestCase(AreaTypeConstants.Chamber, "Chamber")]
        [TestCase(AreaTypeConstants.DeadEnd, "Dead end")]
        [TestCase(AreaTypeConstants.Door, "Door")]
        [TestCase(AreaTypeConstants.Hall, "Hall")]
        [TestCase(AreaTypeConstants.RollAgain, "Roll again")]
        [TestCase(AreaTypeConstants.SidePassage, "Side passage")]
        [TestCase(AreaTypeConstants.Stairs, "Stairs")]
        [TestCase(AreaTypeConstants.Turn, "Turn")]
        public void Constant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
