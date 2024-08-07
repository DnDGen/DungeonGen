﻿using DnDGen.DungeonGen.Models;
using NUnit.Framework;

namespace DnDGen.DungeonGen.Tests.Unit.Models
{
    [TestFixture]
    public class TrapTests
    {
        private Trap trap;

        [SetUp]
        public void Setup()
        {
            trap = new Trap();
        }

        [Test]
        public void TrapIsInitialized()
        {
            Assert.That(trap.ChallengeRating, Is.EqualTo(0));
            Assert.That(trap.Descriptions, Is.Empty);
            Assert.That(trap.SearchDC, Is.EqualTo(0));
            Assert.That(trap.DisableDeviceDC, Is.EqualTo(0));
            Assert.That(trap.Name, Is.Empty);
        }
    }
}
