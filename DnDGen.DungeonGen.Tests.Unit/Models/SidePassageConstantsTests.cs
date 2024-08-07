﻿using DnDGen.DungeonGen.Models;
using NUnit.Framework;

namespace DnDGen.DungeonGen.Tests.Unit.Models
{
    [TestFixture]
    public class SidePassageConstantsTests
    {
        [TestCase(SidePassageConstants.Left90Degrees, "Left, 90 degrees")]
        [TestCase(SidePassageConstants.Right90Degrees, "Right, 90 degrees")]
        [TestCase(SidePassageConstants.Left45DegreesAhead, "Left, 45 degrees ahead")]
        [TestCase(SidePassageConstants.Right45DegreesAhead, "Right, 45 degrees ahead")]
        [TestCase(SidePassageConstants.Left45DegreesBehind, "Left, 45 degrees behind")]
        [TestCase(SidePassageConstants.Right45DegreesBehind, "Right, 45 degrees behind")]
        [TestCase(SidePassageConstants.TIntersection, "T-Intersection")]
        [TestCase(SidePassageConstants.YIntersection, "Y-Intersection")]
        [TestCase(SidePassageConstants.CrossIntersection, "Cross intersection")]
        [TestCase(SidePassageConstants.XIntersection, "X-Intersection")]
        [TestCase(SidePassageConstants.ParallelPassage, "Parallel passage")]
        public void Constant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
