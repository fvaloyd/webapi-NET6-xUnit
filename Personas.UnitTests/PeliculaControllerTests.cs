using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Personas.Api.Controllers;
using Personas.Api.model;
using Personas.Api.repository;
using Xunit;

namespace Pelicula.UnitTests;

public class PeliculaControllerTests
{
    Random randomId = new Random();
    Persona personaMock = new Persona
    {
       Id = 1,
       Name = "Francis",
       Age = 21,
       Email = "francis@francis.com",
       LastName = "Alcantara"
    };
    List<Persona> fakeList = new List<Persona>();
    int invalidId = -1;
    int diferentId = 2;
    int unExistId = 2;

    [Fact]
    public async Task GetAllPersonasAsync_ExpectAListOfPersonas()
    {
        var repositoryStub = new Mock<IPersonaRepository>();
        repositoryStub.Setup(repo => repo.GetAllPersonasAsync()).ReturnsAsync(fakeList);
        
        var controller = new PersonaController(repositoryStub.Object);

        var result = await controller.Get(); 

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetPersonaByIdAsync_withInvalidId_ExpectBadRequest()
    {
        var repositoyStub = new Mock<IPersonaRepository>();
        repositoyStub.Setup(repo => repo.GetPersonaByIdAsync(randomId.Next())).ReturnsAsync((Persona)null!);

        var controller = new PersonaController(repositoyStub.Object);

        var result = await controller.Get(invalidId);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetPersonaByIdAsync_WithUnExistId_ExpectNotFound()
    {
        var repositoryStub = new Mock<IPersonaRepository>();
        repositoryStub.Setup(repo => repo.GetPersonaByIdAsync(randomId.Next())).ReturnsAsync((Persona)null!);

        var controller = new PersonaController(repositoryStub.Object);

        var result = await controller.Get(randomId.Next());

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetPersonaByIdAsync_WithValidId_ExpectOk()
    {
        var repositoryStub = new Mock<IPersonaRepository>();
        repositoryStub.Setup(repo => repo.GetPersonaByIdAsync(personaMock.Id)).ReturnsAsync(personaMock);

        var controller = new PersonaController(repositoryStub.Object);

        var result = await controller.Get(personaMock.Id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task CreatePersonaAsync_WithValidEntity_ExpectACreateAtAction()
    {
        var repositoyStub = new Mock<IPersonaRepository>();
        repositoyStub.Setup(repo => repo.CreatePersonaAsync(personaMock)).ReturnsAsync(1);

        var controller = new PersonaController(repositoyStub.Object);

        var result = await controller.Post(personaMock);

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task CreatePersonaAsync_WithInvalidEntity_ExpectBadRequest()
    {
        var repositorySub = new Mock<IPersonaRepository>();
        repositorySub.Setup(repo => repo.CreatePersonaAsync(personaMock)).ReturnsAsync(1);

        var controller = new PersonaController(repositorySub.Object);

        var result = await controller.Post((Persona)null!);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePersonaAsync_WithDiferentIds_ExpectBadRequest()
    {
        var repositoyStub = new Mock<IPersonaRepository>();
        repositoyStub.Setup(repo => repo.UpdatePersonaAsync(personaMock)).ReturnsAsync(1);

        var controller = new PersonaController(repositoyStub.Object);

        var result = await controller.Put(diferentId, personaMock);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePersonaAsync_WithSameIds_ExpectNoContent()
    {
        var repositoyStub = new Mock<IPersonaRepository>();
        repositoyStub.Setup(repo => repo.UpdatePersonaAsync(personaMock)).ReturnsAsync(1);

        var controller = new PersonaController(repositoyStub.Object);

        var result = await controller.Put(personaMock.Id, personaMock);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeletePersonaAsync_WithUnExistId_ExpectBadRequest()
    {
        var repositoyStub = new Mock<IPersonaRepository>();
        repositoyStub.Setup(repo => repo.DeletePersonaAsync(1)).ReturnsAsync(1);

        var controller = new PersonaController(repositoyStub.Object);

        var result = await controller.Delete(unExistId);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeletePersonaAsync_WithExistId_ExpectNoContent()
    {
        var repositoyStub = new Mock<IPersonaRepository>();
        repositoyStub.Setup(repo => repo.DeletePersonaAsync(personaMock.Id)).ReturnsAsync(1);

        var controller = new PersonaController(repositoyStub.Object);

        var result = await controller.Delete(personaMock.Id);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeletePersonaAsync_WithInvalidId_ExpectBadRequest()
    {
        var repositoyStub = new Mock<IPersonaRepository>();
        repositoyStub.Setup(repo => repo.DeletePersonaAsync(personaMock.Id)).ReturnsAsync(1);

        var controller = new PersonaController(repositoyStub.Object);

        var result = await controller.Delete(invalidId);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
