﻿using DnDGen.DungeonGen.Models;
using NUnit.Framework;

namespace DnDGen.DungeonGen.Tests.Unit.Models
{
    [TestFixture]
    public class DescriptionConstantsTests
    {
        [TestCase(DescriptionConstants.Chimney, "Chimney")]
        [TestCase(DescriptionConstants.Circular, "Circular")]
        [TestCase(DescriptionConstants.DoubleCavern, "Double cavern")]
        [TestCase(DescriptionConstants.Iron, "Iron")]
        [TestCase(DescriptionConstants.MagicallyReinforced, "Magically reinforced")]
        [TestCase(DescriptionConstants.SlidesToSide, "Slides to one side")]
        [TestCase(DescriptionConstants.SlidesDown, "Slides down")]
        [TestCase(DescriptionConstants.SlidesUp, "Slides up")]
        [TestCase(DescriptionConstants.Stone, "Stone")]
        [TestCase(DescriptionConstants.TrapDoor, "Trap door")]
        [TestCase(DescriptionConstants.Wooden, "Wooden")]
        [TestCase(DescriptionConstants.OnTheLeft, "On the left")]
        [TestCase(DescriptionConstants.OnTheRight, "On the right")]
        [TestCase(DescriptionConstants.StraightAhead, "Straight ahead")]
        public void Constant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
