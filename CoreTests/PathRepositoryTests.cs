using System;
using FluentAssertions;
using NUnit.Framework;

namespace Furball.Core.Tests
{
    [TestFixture]
    public class PathRepositoryTests
    {
        private PathRepository _sut;

        [SetUp]
        public void Setup()
        {
            GetSut();
        }

        [Test]
        public void CTorThrowsOnNullTypeList()
        {
            //Arrange

            //Act
            Action action = () => new PathRepository(null);

            //Assert
            action.ShouldThrow<ArgumentNullException>().WithMessage("");
        }

        private void GetSut()
        {
            //_sut = new PathRepository();
        }
    }
}
