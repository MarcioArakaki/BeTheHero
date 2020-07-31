using System;
using Moq;
using Xunit;
using BeTheHero.Persitence;
using BeTheHero.Persitence.Models;
using BeTheHero.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

            // assert
            Assert.IsType<OkObjectResult>(result);
            var resultObject = result.Value as Incident;
            Assert.Equal(resultObject.Id, incidentId);
        }

        [Fact]
        public void GivenGetThenReturnIncidents()
        {
            // act
            var result = Fixture.Controller.Get() as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(result);
            var resultObject = result.Value as List<Incident>;
            Assert.NotEmpty(resultObject);
        }

        [Fact]
        public void GivenCreateThenIncidentIsAdded()
        {
            // arrange
            var incident = new Incident { Id = 1 };

            // act
            var result = Fixture.Controller.Create(incident) as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Contains(incident,Fixture.AllIncidents);
        }

        [Fact]
        public void GivenDeleteThenIncidentIsDeleted()
        {
            // arrange
            var id = 1;

            // act
            var result = Fixture.Controller.Remove(id) as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Null(Fixture.AllIncidents.FirstOrDefault(i => i.Id == id));
        }  

        [Fact]
        public void GivenUpdateThenIncidentIsUpdated()
        {
            // arrange
            var incident = new Incident { Id = 1, Title = "Updated Title"};

            // act
            var result = Fixture.Controller.Update(incident) as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(Fixture.AllIncidents.FirstOrDefault(i => i.Id == incident.Id).Title,incident.Title);
        }      
    }

    public class Fixture : IDisposable
    {
        public IncidentController Controller { get; set; }
        public List<Incident> AllIncidents { get; set; }

        public Mock<IIncidentRepository> IncidentRepositoryMock { get; set; }
        public Fixture()
        {
            this.AllIncidents = new List<Incident>(){
                new Incident
                {
                    Id =1
                },
                new Incident
                {
                    Id =2
                },
                new Incident
                {
                    Id =3
                },
                new Incident
                {
                    Id =4
                }
            };

            this.IncidentRepositoryMock = new Mock<IIncidentRepository>();
            this.IncidentRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new Incident { Id = 42 });
            this.IncidentRepositoryMock.Setup(x => x.Get()).Returns(new List<Incident>() { new Incident { Id = 42 } });
            this.IncidentRepositoryMock.Setup(x => x.Create(It.IsAny<Incident>())).Callback((Incident i) => {this.AllIncidents.Add(i);});
            this.IncidentRepositoryMock.Setup(x => x.Delete(It.IsAny<int>())).Callback((int i) => {this.AllIncidents.RemoveAll(incident => incident.Id == i);});
            this.IncidentRepositoryMock.Setup(x => x.Update(It.IsAny<Incident>())).Callback((Incident i) => {this.AllIncidents.First(incident => incident.Id == i.Id).Title = i.Title;});

            this.Controller = new IncidentController(this.IncidentRepositoryMock.Object);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
