using System;
using Moq;
using Xunit;
using BeTheHero.Persitence;
using BeTheHero.Persitence.Models;
using BeTheHero.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BeTheHero.Tests
{
    public class IncidentControllerTest : IClassFixture<Fixture>
    {
        Fixture Fixture;

        public IncidentControllerTest(Fixture Fixture)
        {
            this.Fixture = Fixture;
        }

        [Fact]
        public void WhenValidIdThenReturnIncident()
        {
            // arrange
            var incidentId = 42;

            // act
            var result = Fixture.Controller.Get(incidentId) as OkObjectResult;

            // asserrt
            Assert.IsType<OkObjectResult>(result);
            var resultObject = result.Value as Incident;
            Assert.Equal(resultObject.Id, incidentId);
        }
    }

    public class Fixture : IDisposable
    {
        public IncidentController Controller { get; set; }

        public Mock<IIncidentRepository> IncidentRepositoryMock { get; set; }
        public Fixture()
        {
            this.IncidentRepositoryMock = new Mock<IIncidentRepository>();
            this.IncidentRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new Incident { Id = 42 });
            this.Controller = new IncidentController(this.IncidentRepositoryMock.Object);

        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
