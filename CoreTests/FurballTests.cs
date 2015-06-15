using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Furball.Core.Tests
{
    [TestFixture]
    public class FurballTests
    {
        private global::Furball.Core.Furball _sut;

        [SetUp]
        public void Setup()
        {
            GetSut();
        }

        [Test]
        public void CTorThrowsOnNullNext()
        {
            //Arrange

            //Act
            Action action = () => new global::Furball.Core.Furball(null);

            //Assert
            action.ShouldThrow<ArgumentNullException>();
        }
        
        private void GetSut()
        {
            var next = new Func<IDictionary<string, object>, Task>(TestFunction);
            _sut = new global::Furball.Core.Furball(next);
        }

        public Task TestFunction(IDictionary<string, object> environment)
        {
            return Task.FromResult(0);
        }
    }
}
