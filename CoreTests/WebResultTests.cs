using System;
using System.Net;
using FluentAssertions;
using Furball.Common;
using NUnit.Framework;

namespace Furball.Core.Tests
{
    [TestFixture]
    public class WebResultTests
    {
        private WebResult _sut;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CTorInitializesListOfHeadersWhenNoParametersAreProvided()
        {
            //Arrange

            //Act
            var sut = new WebResult();

            //Assert
            sut.Headers.Should().NotBeNull();
        }

        [Test]
        public void CTorInitializesListOfHeadersWhenOnlyResultIsProvided()
        {
            //Arrange
            const int result = 1;

            //Act
            var sut = new WebResult(result);

            //Assert
            sut.Headers.Should().NotBeNull();
        }

        [Test]
        public void CTorInitializesListOfHeadersWhenOnlyStatusIsProvided()
        {
            //Arrange
            const HttpStatusCode status = HttpStatusCode.OK;

            //Act
            var sut = new WebResult(status);

            //Assert
            sut.Headers.Should().NotBeNull();
        }

        [Test]
        public void CTorInitializesListOfHeadersWhenResultAndStatusAreProvided()
        {
            //Arrange
            const int result = 1;
            const HttpStatusCode status = HttpStatusCode.OK;

            //Act
            var sut = new WebResult(result, status);

            //Assert
            sut.Headers.Should().NotBeNull();
        }

        [Test]
        public void CTorSetsStatusOfOkIfResultIsNotNullAndStatusIsNotProvided()
        {
            //Arrange
            const int result = 1;

            //Act
            var sut = new WebResult(result);

            //Assert
            sut.Status.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void CTorSetsStatusOfNoContentIfResultIsNullAndStatusIsNotProvided()
        {
            //Arrange
            const object result = null;

            //Act
            var sut = new WebResult(result);

            //Assert
            sut.Status.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public void CTorSetsStatusOfNoContentIfResultAndStatusAreNotProvided()
        {
            //Arrange

            //Act
            var sut = new WebResult();

            //Assert
            sut.Status.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public void CTorSetsResultToNullIfResultIsNotProvided()
        {
            //Arrange
            const HttpStatusCode status = HttpStatusCode.NoContent;

            //Act
            var sut = new WebResult(status);

            //Assert
            sut.Result.Should().BeNull();
        }
    }
}
