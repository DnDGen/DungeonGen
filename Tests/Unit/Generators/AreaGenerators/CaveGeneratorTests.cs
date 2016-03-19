﻿using DungeonGen.Common;
using DungeonGen.Generators;
using DungeonGen.Generators.Domain.AreaGenerators;
using DungeonGen.Selectors;
using DungeonGen.Tables;
using EncounterGen.Common;
using EncounterGen.Generators;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DungeonGen.Tests.Unit.Generators.AreaGenerators
{
    [TestFixture]
    public class CaveGeneratorTests
    {
        private AreaGenerator caveGenerator;
        private Mock<IAreaPercentileSelector> mockAreaPercentileSelector;
        private Area selectedCave;
        private Mock<PoolGenerator> mockPoolGenerator;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IEncounterGenerator> mockEncounterGenerator;

        [SetUp]
        public void Setup()
        {
            mockAreaPercentileSelector = new Mock<IAreaPercentileSelector>();
            mockPoolGenerator = new Mock<PoolGenerator>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockEncounterGenerator = new Mock<IEncounterGenerator>();
            caveGenerator = new CaveGenerator(mockAreaPercentileSelector.Object, mockPoolGenerator.Object, mockPercentileSelector.Object, mockEncounterGenerator.Object);
            selectedCave = new Area();

            mockAreaPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Caves)).Returns(selectedCave);
        }

        [Test]
        public void ReturnCave()
        {
            var caves = caveGenerator.Generate(9266, 90210);
            Assert.That(caves, Is.Not.Null);
            Assert.That(caves, Is.Not.Empty);
        }

        [Test]
        public void GenerateCave()
        {
            var caves = caveGenerator.Generate(9266, 90210);
            Assert.That(caves.Single(), Is.EqualTo(selectedCave));
        }

        [Test]
        public void GenerateDoubleCave()
        {
            selectedCave.Descriptions = new[] { DescriptionConstants.DoubleCavern };
            selectedCave.Contents.Miscellaneous = new[] { "42", "600" };

            var caves = caveGenerator.Generate(9266, 90210);
            Assert.That(caves.Count(), Is.EqualTo(2));

            var first = caves.First();
            var last = caves.Last();

            Assert.That(first, Is.EqualTo(selectedCave));
            Assert.That(first.Descriptions, Is.Empty);
            Assert.That(first.Contents.IsEmpty, Is.True);
            Assert.That(last.Length, Is.EqualTo(42));
            Assert.That(last.Width, Is.EqualTo(600));
            Assert.That(last.Contents.IsEmpty, Is.True);
            Assert.That(last.Descriptions, Is.Empty);
            Assert.That(last.Type, Is.EqualTo(AreaTypeConstants.Cave));
        }

        [Test]
        public void GenerateCaveWithPool()
        {
            selectedCave.Contents.Miscellaneous = new[] { ContentsTypeConstants.Pool };

            var pool = new Pool();
            mockPoolGenerator.Setup(g => g.Generate(90210)).Returns(pool);

            var cave = caveGenerator.Generate(9266, 90210).Single();
            Assert.That(cave, Is.EqualTo(selectedCave));
            Assert.That(cave.Contents.Pool, Is.EqualTo(pool));
        }

        [Test]
        public void GenerateDoubleCaveWithPool()
        {
            selectedCave.Descriptions = new[] { DescriptionConstants.DoubleCavern };
            selectedCave.Contents.Miscellaneous = new[] { "42", "600", ContentsTypeConstants.Pool };

            var pool = new Pool();
            mockPoolGenerator.Setup(g => g.Generate(90210)).Returns(pool);

            var caves = caveGenerator.Generate(9266, 90210);
            Assert.That(caves.Count(), Is.EqualTo(2));

            var first = caves.First();
            var last = caves.Last();

            Assert.That(first, Is.EqualTo(selectedCave));
            Assert.That(first.Descriptions, Is.Empty);
            Assert.That(first.Contents.IsEmpty, Is.True);
            Assert.That(last.Length, Is.EqualTo(42));
            Assert.That(last.Width, Is.EqualTo(600));
            Assert.That(last.Contents.IsEmpty, Is.False);
            Assert.That(last.Contents.Pool, Is.EqualTo(pool));
            Assert.That(last.Contents.Miscellaneous.Single(), Is.EqualTo(ContentsTypeConstants.Pool));
            Assert.That(last.Descriptions, Is.Empty);
            Assert.That(last.Type, Is.EqualTo(AreaTypeConstants.Cave));
        }

        [Test]
        public void GenerateCaveWithNoLake()
        {
            selectedCave.Contents.Miscellaneous = new[] { ContentsTypeConstants.Lake };

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Lakes)).Returns(string.Empty);

            var cave = caveGenerator.Generate(9266, 90210).Single();
            Assert.That(cave, Is.EqualTo(selectedCave));
            Assert.That(cave.Contents.Miscellaneous, Is.Empty);
        }

        [Test]
        public void GenerateCaveWithLake()
        {
            selectedCave.Contents.Miscellaneous = new[] { ContentsTypeConstants.Lake };

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Lakes)).Returns("laketown");

            var cave = caveGenerator.Generate(9266, 90210).Single();
            Assert.That(cave, Is.EqualTo(selectedCave));
            Assert.That(cave.Contents.Miscellaneous.Single(), Is.EqualTo("laketown"));
        }

        [Test]
        public void GenerateCaveWithLakeAndEncounter()
        {
            selectedCave.Contents.Miscellaneous = new[] { ContentsTypeConstants.Lake };

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Lakes)).Returns("laketown/Encounter");

            var encounter = new Encounter();
            mockEncounterGenerator.Setup(g => g.Generate(EnvironmentConstants.Dungeon, 90210)).Returns(encounter);

            var cave = caveGenerator.Generate(9266, 90210).Single();
            Assert.That(cave, Is.EqualTo(selectedCave));
            Assert.That(cave.Contents.Miscellaneous.First(), Is.EqualTo("laketown"));
            Assert.That(cave.Contents.Miscellaneous.Last(), Is.EqualTo(ContentsTypeConstants.Encounter));
            Assert.That(cave.Contents.Miscellaneous.Count(), Is.EqualTo(2));
            Assert.That(cave.Contents.Encounters.Single(), Is.EqualTo(encounter));
        }
    }
}
