using FluentAssertions;
using NUnit.Framework;

namespace Furball.Core.Tests
{
    [TestFixture]
    public class FurballOptionsTests
    {
        private FurballOptions _sut;

        [SetUp]
        public void Setup()
        {
            GetSut();
        }

        [Test]
        public void HandlerErrorsDefaultsToDontSendErrorsInCTor()
        {
            //Arrange

            //Act

            //Assert
            _sut.HandlerErrors.Should().Be(HandlerErrorTypes.DontSendErrors);
        }

        private void GetSut()
        {
            _sut = new FurballOptions();
        }
    }
}
