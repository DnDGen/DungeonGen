﻿using DungeonGen.Domain.Generators;
using DungeonGen.Domain.Generators.AreaGenerators;
using DungeonGen.Domain.Generators.ContentGenerators;
using DungeonGen.Domain.Generators.Dungeons;
using DungeonGen.Domain.Generators.ExitGenerators;
using DungeonGen.Domain.Generators.Factories;
using EncounterGen.Generators;
using NUnit.Framework;
using TreasureGen.Generators;

namespace DungeonGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class GeneratorModuleTests : IoCTests
    {
        [Test]
        public void DungeonGeneratorIsInjectedAndDecorated()
        {
            AssertInstanceOf<IDungeonGenerator, DungeonGeneratorEventDecorator>();
        }

        [Test]
        public void AreaGeneratorFactoryIsInjected()
        {
            AssertInstanceOf<AreaGeneratorFactory, DomainAreaGeneratorFactory>();
        }

        [Test]
        public void TrapGeneratorIsInjected()
        {
            AssertInstanceOf<ITrapGenerator, TrapGenerator>();
        }

        [Test]
        public void SpecialAreaGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Special);
        }

        [Test]
        public void ChamberGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Chamber);
        }

        [Test]
        public void DoorGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Door);
        }

        [Test]
        public void HallGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Hall);
        }

        [Test]
        public void RoomGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Room);
        }

        [Test]
        public void SidePassageGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.SidePassage);
        }

        [Test]
        public void StairsGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Stairs);
        }

        [Test]
        public void TurnGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Turn);
        }

        [Test]
        public void RoomExitGeneratorIsInjected()
        {
            AssertInstanceOf<ExitGenerator, RoomExitGenerator>(AreaTypeConstants.Room);
        }

        [Test]
        public void ChamberExitGeneratorIsInjected()
        {
            AssertInstanceOf<ExitGenerator, ChamberExitGenerator>(AreaTypeConstants.Chamber);
        }

        [Test]
        public void ContentsGeneratorIsInjected()
        {
            AssertInstanceOf<ContentsGenerator, DomainContentsGenerator>();
        }

        [Test]
        public void PoolGeneratorIsInjected()
        {
            AssertInstanceOf<PoolGenerator, DomainPoolGenerator>();
        }

        [Test]
        public void CaveGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(AreaTypeConstants.Cave);
        }

        [Test]
        public void ParallelPassageGeneratorIsInjected()
        {
            AssertInstanceOf<AreaGenerator, AreaGeneratorEventDecorator>(SidePassageConstants.ParallelPassage);
        }

        [Test]
        public void EXTERNAL_TreasureGeneratorIsInjected()
        {
            AssertInstanceOf<ITreasureGenerator>();
        }

        [Test]
        public void EXTERNAL_EncounterGeneratorIsInjected()
        {
            AssertInstanceOf<IEncounterGenerator>();
        }
    }
}
