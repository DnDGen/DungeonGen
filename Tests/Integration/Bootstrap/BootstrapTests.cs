﻿using DungeonGen.Tests.Integration.Common;
using NUnit.Framework;

namespace DungeonGen.Tests.Integration.Bootstrap
{
    [Bootstrap]
    public abstract class BootstrapTests : IntegrationTests
    {
        protected void AssertSingleton<T>()
        {
            var first = GetNewInstanceOf<T>();
            var second = GetNewInstanceOf<T>();
            Assert.That(first, Is.EqualTo(second));
        }

        protected void AssertInstanceOf<I, T>(string name)
        {
            var instance = GetNewInstanceOf<I>(name);
            Assert.That(instance, Is.Not.Null);
            Assert.That(instance, Is.InstanceOf<T>());
        }

        protected void AssertInstanceOf<I, T>()
        {
            var instance = GetNewInstanceOf<I>();
            Assert.That(instance, Is.Not.Null);
            Assert.That(instance, Is.InstanceOf<T>());
        }
    }
}
