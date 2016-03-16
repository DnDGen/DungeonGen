﻿using DungeonGen.Common;
using DungeonGen.Generators;
using DungeonGen.Generators.Domain.AreaGenerators;
using DungeonGen.Selectors;
using DungeonGen.Tables;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace DungeonGen.Tests.Unit.Generators.AreaGenerators
{
    [TestFixture]
    public class StairsGeneratorTests
    {
        private AreaGenerator stairsGenerator;
        private Mock<IAreaPercentileSelector> mockAreaPercentileSelector;
        private Area selectedStairs;
        private Mock<Dice> mockDice;
        private Mock<AreaGenerator> mockChamberGenerator;

        [SetUp]
        public void Setup()
        {
            mockAreaPercentileSelector = new Mock<IAreaPercentileSelector>();
            mockDice = new Mock<Dice>();
            mockChamberGenerator = new Mock<AreaGenerator>();
            stairsGenerator = new StairsGenerator(mockAreaPercentileSelector.Object, mockDice.Object, mockChamberGenerator.Object);
            selectedStairs = new Area();

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Stairs)).Returns(selectedStairs);
        }

        [Test]
        public void GenerateStairs()
        {
            var stairs = stairsGenerator.Generate(9266, 90210);
            Assert.That(stairs, Is.Not.Null);
            Assert.That(stairs, Is.Not.Empty);
            Assert.That(stairs.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GenerateStairsDownXLevels()
        {
            var selectedStairs = new Area();
            selectedStairs.Length = 600;

            var stairs = stairsGenerator.Generate(9266, 90210).Single();
            Assert.That(stairs, Is.EqualTo(selectedStairs));
            Assert.That(stairs.Descriptions.Single(), Is.EqualTo("Down to level 9866"));
        }

        [TestCase(41)]
        [TestCase(42)]
        public void GenerateStairsDownXLevelsDeadEnds(int roll)
        {
            var selectedStairs = new Area();
            selectedStairs.Length = 600;
            selectedStairs.Width = 42;

            mockDice.Setup(d => d.Roll(1).IndividualRolls(100)).Returns(new[] { roll });

            var stairs = stairsGenerator.Generate(9266, 90210);
            Assert.That(stairs.Count(), Is.EqualTo(2));

            var first = stairs.First();
            var last = stairs.Last();

            Assert.That(first, Is.EqualTo(selectedStairs));
            Assert.That(first.Descriptions.Single(), Is.EqualTo("Down to level 9866"));
            Assert.That(last.Type, Is.EqualTo(AreaTypeConstants.DeadEnd));
            Assert.That(last.Contents.IsEmpty, Is.True);
            Assert.That(last.Descriptions, Is.Empty);
            Assert.That(last.Length, Is.EqualTo(0));
            Assert.That(last.Width, Is.EqualTo(0));
        }

        [Test]
        public void GenerateStairsDownXLevelsDoesNotDeadEnd()
        {
            var selectedStairs = new Area();
            selectedStairs.Length = 600;
            selectedStairs.Width = 42;

            mockDice.Setup(d => d.Roll(1).IndividualRolls(100)).Returns(new[] { 43 });

            var stairs = stairsGenerator.Generate(9266, 90210).Single();
            Assert.That(stairs, Is.EqualTo(selectedStairs));
            Assert.That(stairs.Descriptions.Single(), Is.EqualTo("Down to level 9866"));
        }

        [Test]
        public void GenerateStairsUpXLevels()
        {
            var selectedStairs = new Area();
            selectedStairs.Length = -42;

            var stairs = stairsGenerator.Generate(9266, 90210).Single();
            Assert.That(stairs, Is.EqualTo(selectedStairs));
            Assert.That(stairs.Descriptions.Single(), Is.EqualTo("Up to level 9224"));
        }

        [TestCase(-9266)]
        [TestCase(-9267)]
        public void IfUpTooManyLevels_ThenDeadEnd(int levelsDown)
        {
            var selectedStairs = new Area();
            selectedStairs.Length = levelsDown;

            var stairs = stairsGenerator.Generate(9266, 90210).Single();
            Assert.That(stairs.Type, Is.EqualTo(AreaTypeConstants.DeadEnd));
            Assert.That(stairs.Contents.IsEmpty, Is.True);
            Assert.That(stairs.Descriptions, Is.Empty);
            Assert.That(stairs.Length, Is.EqualTo(0));
            Assert.That(stairs.Width, Is.EqualTo(0));
        }

        [Test]
        public void ChimneyAlsoLetsTheHallContinue()
        {
            var selectedStairs = new Area();
            selectedStairs.Length = -600;
            selectedStairs.Descriptions = new[] { DescriptionConstants.Chimney };

            var stairs = stairsGenerator.Generate(9266, 90210);
            Assert.That(stairs.Count(), Is.EqualTo(2));

            var first = stairs.First();
            var last = stairs.Last();

            Assert.That(first.Type, Is.EqualTo(AreaTypeConstants.Hall));
            Assert.That(first.Contents.IsEmpty, Is.True);
            Assert.That(first.Descriptions, Is.Empty);
            Assert.That(first.Length, Is.EqualTo(30));
            Assert.That(first.Width, Is.EqualTo(0));
            Assert.That(last, Is.EqualTo(selectedStairs));
            Assert.That(last.Descriptions, Contains.Item("Up to level 8666"));
            Assert.That(last.Descriptions, Contains.Item(DescriptionConstants.Chimney));
            Assert.That(last.Descriptions.Count(), Is.EqualTo(2));
        }

        [TestCase(-9266)]
        [TestCase(-9267)]
        public void IfChimneyGoesUpTooHigh_NoChimney(int levelsDown)
        {
            var selectedStairs = new Area();
            selectedStairs.Length = levelsDown;
            selectedStairs.Descriptions = new[] { DescriptionConstants.Chimney };

            var stairs = stairsGenerator.Generate(9266, 90210).Single();
            Assert.That(stairs.Type, Is.EqualTo(AreaTypeConstants.Hall));
            Assert.That(stairs.Contents.IsEmpty, Is.True);
            Assert.That(stairs.Descriptions, Is.Empty);
            Assert.That(stairs.Length, Is.EqualTo(30));
            Assert.That(stairs.Width, Is.EqualTo(0));
        }

        [Test]
        public void TrapDoorAlsoLetsTheHallContinue()
        {
            var selectedStairs = new Area();
            selectedStairs.Length = 600;
            selectedStairs.Descriptions = new[] { DescriptionConstants.TrapDoor };

            var stairs = stairsGenerator.Generate(9266, 90210);
            Assert.That(stairs.Count(), Is.EqualTo(2));

            var first = stairs.First();
            var last = stairs.Last();

            Assert.That(first.Type, Is.EqualTo(AreaTypeConstants.Hall));
            Assert.That(first.Contents.IsEmpty, Is.True);
            Assert.That(first.Descriptions, Is.Empty);
            Assert.That(first.Length, Is.EqualTo(30));
            Assert.That(first.Width, Is.EqualTo(0));
            Assert.That(last, Is.EqualTo(selectedStairs));
            Assert.That(last.Descriptions, Contains.Item("Down to level 9866"));
            Assert.That(last.Descriptions, Contains.Item(DescriptionConstants.TrapDoor));
            Assert.That(last.Descriptions.Count(), Is.EqualTo(2));
        }

        [Test]
        public void StairsEndInChamber()
        {
            var selectedStairs = new Area();
            selectedStairs.Length = 600;
            selectedStairs.Contents.Miscellaneous = new[] { AreaTypeConstants.Chamber };

            var chamber = new Area();
            var exit = new Area();

            mockChamberGenerator.Setup(g => g.Generate(9266, 90210)).Returns(new[] { chamber, exit });

            var stairs = stairsGenerator.Generate(9266, 90210);
            Assert.That(stairs.Count(), Is.EqualTo(3));
            Assert.That(stairs, Contains.Item(selectedStairs));
            Assert.That(stairs, Contains.Item(chamber));
            Assert.That(stairs, Contains.Item(exit));

            var first = stairs.First();

            Assert.That(first, Is.EqualTo(selectedStairs));
            Assert.That(first.Descriptions.Single(), Is.EqualTo("Down to level 9866"));
            Assert.That(first.Contents.IsEmpty, Is.True);
        }
    }
}
