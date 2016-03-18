﻿using DungeonGen.Bootstrap.Factories;
using DungeonGen.Common;
using DungeonGen.Generators;
using DungeonGen.Generators.Domain;
using DungeonGen.Generators.Domain.AreaGenerators;
using DungeonGen.Generators.Domain.ContentGenerators;
using DungeonGen.Generators.Domain.RuntimeFactories;
using Ninject.Modules;

namespace DungeonGen.Bootstrap.Modules
{
    public class GeneratorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDungeonGenerator>().To<DungeonGenerator>();
            Bind<IAreaGeneratorFactory>().ToMethod(c => AreaGeneratorFactoryFactory.Build(c.Kernel));
            Bind<ITrapGenerator>().To<TrapGenerator>();
            Bind<ContentsGenerator>().To<DomainContentsGenerator>();
            Bind<PoolGenerator>().To<DomainPoolGenerator>();

            Bind<AreaGenerator>().To<CaveGenerator>().Named(AreaTypeConstants.Cave);
            Bind<AreaGenerator>().ToMethod(c => ChamberGeneratorFactory.Build(c.Kernel)).Named(AreaTypeConstants.Chamber);
            Bind<AreaGenerator>().To<DoorGenerator>().Named(AreaTypeConstants.Door);
            Bind<AreaGenerator>().To<HallGenerator>().Named(AreaTypeConstants.Hall);
            Bind<AreaGenerator>().ToMethod(c => RoomGeneratorFactory.Build(c.Kernel)).Named(AreaTypeConstants.Room);
            Bind<AreaGenerator>().ToMethod(c => SidePassageGeneratorFactory.Build(c.Kernel)).Named(AreaTypeConstants.SidePassage);
            Bind<AreaGenerator>().ToMethod(c => SpecialAreaGeneratorFactory.Build(c.Kernel)).Named(AreaTypeConstants.Special);
            Bind<AreaGenerator>().ToMethod(c => StairsGeneratorFactory.Build(c.Kernel)).Named(AreaTypeConstants.Stairs);
            Bind<AreaGenerator>().To<TurnGenerator>().Named(AreaTypeConstants.Turn);

            Bind<ExitGenerator>().ToMethod(c => ChamberExitGeneratorFactory.Build(c.Kernel)).Named(AreaTypeConstants.Chamber);
            Bind<ExitGenerator>().ToMethod(c => RoomExitGeneratorFactory.Build(c.Kernel)).Named(AreaTypeConstants.Room);
        }
    }
}
