﻿using DnDGen.DungeonGen.Generators.AreaGenerators;
using DnDGen.DungeonGen.Models;
using DnDGen.DungeonGen.Selectors;
using DnDGen.DungeonGen.Tables;
using DnDGen.EncounterGen.Generators;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.DungeonGen.Tests.Unit.Generators.AreaGenerators
{
    [TestFixture]
    public class HallGeneratorTests
    {
        private AreaGenerator hallGenerator;
        private Mock<IAreaPercentileSelector> mockAreaPercentileSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<Dice> mockDice;
        private EncounterSpecifications specifications;

        [SetUp]
        public void Setup()
        {
            mockAreaPercentileSelector = new Mock<IAreaPercentileSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockDice = new Mock<Dice>();
            hallGenerator = new HallGenerator(mockAreaPercentileSelector.Object, mockPercentileSelector.Object, mockDice.Object);

            specifications = new EncounterSpecifications();
        }

        [Test]
        public void AreaTypeIsHall()
        {
            Assert.That(hallGenerator.AreaType, Is.EqualTo(AreaTypeConstants.Hall));
        }

        [Test]
        public void GenerateHall()
        {
            var selectedHall = new Area();
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(selectedHall));
        }

        [Test]
        public void GenerateSpecialHall()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
        }

        [Test]
        public void GenerateSpecialHallWithGalleryStairs()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            specialHall.Contents.Miscellaneous = new[] { ContentsConstants.Gallery };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.GalleryStairs)).Returns("escalator");

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.Gallery));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item("escalator"));
            Assert.That(hall.Contents.Miscellaneous.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GenerateSpecialHallWithGalleryStairsAtBothEnds()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            specialHall.Contents.Miscellaneous = new[] { ContentsConstants.Gallery };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.GalleryStairs)).Returns(ContentsConstants.GalleryStairs_Beginning);
            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.AdditionalGalleryStairs)).Returns("escalator");

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.Gallery));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.GalleryStairs_Beginning));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item("escalator"));
            Assert.That(hall.Contents.Miscellaneous.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenerateSpecialHallWithStream()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            specialHall.Contents.Miscellaneous = new[] { ContentsConstants.Stream };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.StreamCrossing)).Returns("ferry");

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.Stream));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item("ferry"));
            Assert.That(hall.Contents.Miscellaneous.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GenerateSpecialHallWithStreamWithoutCrossing()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            specialHall.Contents.Miscellaneous = new[] { ContentsConstants.Stream };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.StreamCrossing)).Returns(string.Empty);

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.Stream));
            Assert.That(hall.Contents.Miscellaneous.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GenerateSpecialHallWithRiver()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            specialHall.Contents.Miscellaneous = new[] { ContentsConstants.River };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.RiverCrossing)).Returns("ferry");

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.River));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item("ferry"));
            Assert.That(hall.Contents.Miscellaneous.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GenerateSpecialHallWithChasm()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            specialHall.Contents.Miscellaneous = new[] { ContentsConstants.Chasm };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.ChasmCrossing)).Returns("zip line");

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.Chasm));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item("zip line"));
            Assert.That(hall.Contents.Miscellaneous.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GenerateSpecialHallWithChasmCrossingAndRoll()
        {
            var selectedHall = new Area();
            selectedHall.Type = AreaTypeConstants.Special;

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Halls)).Returns(selectedHall);

            var specialHall = new Area();
            specialHall.Contents.Miscellaneous = new[] { ContentsConstants.Chasm };

            var tableName = string.Format(TableNameConstants.SpecialAREA, AreaTypeConstants.Hall);
            mockAreaPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialHall);

            mockPercentileSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.ChasmCrossing)).Returns("zip line");
            mockDice.Setup(d => d.ContainsRoll("zip line", false)).Returns(true);
            mockDice.Setup(d => d.ReplaceExpressionWithTotal("zip line", false)).Returns("roll replaced");

            var hall = hallGenerator.Generate(9266, specifications).Single();
            Assert.That(hall, Is.EqualTo(specialHall));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item(ContentsConstants.Chasm));
            Assert.That(hall.Contents.Miscellaneous, Contains.Item("roll replaced"));
            Assert.That(hall.Contents.Miscellaneous.Count(), Is.EqualTo(2));
        }
    }
}
