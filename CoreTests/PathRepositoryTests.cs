using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Furball.Core.Exceptions;
using NUnit.Framework;

namespace Furball.Core.Tests
{
    [TestFixture]
    public class PathRepositoryTests
    {
        private PathRepository _sut;
        private List<Path> _paths;

        [SetUp]
        public void Setup()
        {
            GetSut();
        }

        [Test]
        public void CTorThrowsOnNullPathList()
        {
            //Arrange

            //Act
            Action action = () => new PathRepository(null);
            
            //Assert
            action.ShouldThrow<ArgumentNullException>().WithMessage("*paths*");
        }

        [Test]
        public async void GetReturnsSingleRequestedPath()
        {
            //Arrange
            const string path = "/";
            const string webMethod = "get";
            var parameters = new Dictionary<string, object>();

            //Act
            var result = await _sut.GetMethodAsync(path, webMethod, parameters);

            //Assert
            result.Should().BeOfType<RequestedPath>();
        }

        [Test]
        public async void GetReturnsWebMethodWithMatchingParameters()
        {
            //Arrange
            const string path = "/";
            const string webMethod = "get";
            var parameters = new Dictionary<string, object> {{"id", 1}};

            //Act
            var result = await _sut.GetMethodAsync(path, webMethod, parameters);

            //Assert
            result.Should().NotBeNull();
            result.Parameters.Count.Should().Be(parameters.Count);
        }

        [Test]
        public async void GetReturnsNonNullInstanceOfTheController()
        {
            //Arrange
            const string path = "/";
            const string httpMethod = "get";
            var parameters = new Dictionary<string, object>();

            //Act
            var result = await _sut.GetMethodAsync(path, httpMethod, parameters);

            //Assert
            result.Instance.Should().NotBeNull();
        }

        [Test]
        public void GetThrowsPathNotFoundErrorIfPathNotFound()
        {
            //Arrange
            const string path = "/badpath";
            const string webMethod = "get";
            var parameters = new Dictionary<string, object>();

            //Act
            Func<Task<RequestedPath>> action = async () => await _sut.GetMethodAsync(path, webMethod, parameters);

            //Assert
            action.ShouldThrow<PathNotFoundException>();
        }

        [Test]
        public void GetThrowsIncorrectWebMethodExceptionIfTheWrongHttpMethodIsRequested()
        {
            //Arrange
            const string path = "/";
            const string webMethod = "badWebMethod";
            var parameters = new Dictionary<string, object>();

            //Act
            Func<Task<RequestedPath>> action = async () => await _sut.GetMethodAsync(path, webMethod, parameters);

            //Assert
            action.ShouldThrow<IncorrectWebMethodException>();
        }

        private void GetSut()
        {
            _paths = new List<Path>
            {
                new Path
                {
                    RequestPath = "/",
                    ControllerType = typeof(TestController),
                    WebMethods = new List<WebMethod>
                    {
                        new WebMethod
                        {
                            HttpMethod = "get",
                            Parameters = new List<Parameter>()
                        },
                        new WebMethod
                        {
                            HttpMethod = "get",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "id",
                                    Type = typeof(int)
                                }
                            }
                        }
                    }
                }
            };

            _sut = new PathRepository(_paths);
        }
    }
}
