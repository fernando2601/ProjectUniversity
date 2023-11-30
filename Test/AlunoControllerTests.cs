using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectUniversity.Controllers;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class AlunoControllerTests
{
    [Fact]
    public async Task Get_ReturnsListOfAlunos()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        var expectedAlunos = new List<Aluno> { new Aluno { Id = Guid.NewGuid(), Nome = "Aluno1" }, new Aluno { Id = Guid.NewGuid(), Nome = "Aluno2" } };
        mockAlunoService.Setup(service => service.ObterTodosAlunosAsync()).ReturnsAsync(expectedAlunos);

        var alunoController = new AlunoController(mockAlunoService.Object);

        var result = await alunoController.Get();

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAlunos = Assert.IsAssignableFrom<IEnumerable<Aluno>>(okObjectResult.Value);
        Assert.Equal(expectedAlunos.Count, returnedAlunos.Count());
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenAlunoNotFound()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        mockAlunoService.Setup(service => service.ObterAlunoPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Aluno)null);

        var alunoController = new AlunoController(mockAlunoService.Object);

        var result = await alunoController.Get(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Post_ReturnsOkObjectResult_WhenAlunoIsAdded()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        var alunoToAdd = new Aluno { Nome = "Novo Aluno" };
        mockAlunoService.Setup(service => service.AdicionarAlunoAsync(It.IsAny<Aluno>())).ReturnsAsync(1);

        var alunoController = new AlunoController(mockAlunoService.Object);

        var result = await alunoController.Post(alunoToAdd);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(1, (int)okObjectResult.Value);
    }

    [Fact]
    public async Task Put_ReturnsOkResult_WhenAlunoIsUpdated()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        var alunoToUpdate = new Aluno { Id = Guid.NewGuid(), Nome = "Aluno Atualizado" };
        mockAlunoService.Setup(service => service.AtualizarAlunoAsync(It.IsAny<Aluno>())).ReturnsAsync(true);

        var alunoController = new AlunoController(mockAlunoService.Object);

        var result = await alunoController.Put(alunoToUpdate.Id, alunoToUpdate);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsOkResult_WhenAlunoIsDeleted()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        mockAlunoService.Setup(service => service.DeletarAlunoAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        var alunoController = new AlunoController(mockAlunoService.Object);

        var result = await alunoController.Delete(Guid.NewGuid());

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True((bool)okResult.Value);
    }
}