﻿using DungeonGen.Common;
using DungeonGen.Generators;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGen.Tests.Integration.Stress
{
    [TestFixture]
    public class DungeonGeneratorTests : StressTests
    {
        [Inject]
        public IDungeonGenerator DungeonGenerator { get; set; }
        [Inject]
        public Random Random { get; set; }

        [TestCase("Dungeon Generator - From Hall")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var areas = GenerateFromHall();
            Assert.That(areas, Is.Not.Empty);

            foreach (var area in areas)
                AssertArea(area);
        }

        private IEnumerable<Area> GenerateFromHall()
        {
            var dungeonLevel = Random.Next(20) + 1;
            var partyLevel = Random.Next(20) + 1;
            return DungeonGenerator.GenerateFromHall(dungeonLevel, partyLevel);
        }

        private void AssertArea(Area area)
        {
            Assert.That(area, Is.Not.Null);
            Assert.That(area.Type, Is.Not.Empty);
            Assert.That(area.Contents, Is.Not.Null);
            Assert.That(area.Descriptions, Is.Not.Null);

            if (area.Type == AreaTypeConstants.General)
            {
                Assert.That(area.Contents.IsEmpty, Is.False);
            }
            else if (area.Type != AreaTypeConstants.DeadEnd && area.Type != AreaTypeConstants.Door && area.Type != AreaTypeConstants.Stairs)
            {
                Assert.That(area.Length, Is.Positive, area.Type);
                Assert.That(area.Width, Is.Not.Negative, area.Type);
            }
        }

        [Test]
        public void StressFromDoor()
        {
            Stress(AssertFromDoor);
        }

        private void AssertFromDoor()
        {
            var dungeonLevel = Random.Next(20) + 1;
            var partyLevel = Random.Next(20) + 1;
            var areas = DungeonGenerator.GenerateFromDoor(dungeonLevel, partyLevel);
            Assert.That(areas, Is.Not.Empty);

            foreach (var area in areas)
                AssertArea(area);
        }

        [Test]
        public void ContinuingHallHasSamePassageWidth()
        {
            Stress(AssertHallContinuesAtSameWidth);
        }

        private void AssertHallContinuesAtSameWidth()
        {
            var dungeonLevel = Random.Next(20) + 1;
            var partyLevel = Random.Next(20) + 1;
            var areas = DungeonGenerator.GenerateFromDoor(dungeonLevel, partyLevel);
            Assert.That(areas, Is.Not.Empty);

            if (areas.Count() == 1)
            {
                var area = areas.Single();
                if (area.Type == AreaTypeConstants.Hall)
                    Assert.That(area.Width, Is.EqualTo(0));
            }
        }
    }
}
