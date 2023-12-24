using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        var expectedAlunos = new List<Aluno> { new Aluno { IdAluno = 1, Nome = "Aluno1" }, new Aluno { IdAluno = 2, Nome = "Aluno2" } };
        mockAlunoService.Setup(service => service.ObterTodosAlunosAsync()).ReturnsAsync(expectedAlunos);

        var mockLogger = new Mock<ILogger<AlunoController>>();

        var alunoController = new AlunoController(mockAlunoService.Object, mockLogger.Object);

        var result = await alunoController.Get();

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAlunos = Assert.IsAssignableFrom<IEnumerable<Aluno>>(okObjectResult.Value);
        Assert.Equal(expectedAlunos.Count, returnedAlunos.Count());
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenAlunoNotFound()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        mockAlunoService.Setup(service => service.ObterAlunoPorIdAsync(It.IsAny<int>())).ReturnsAsync((Aluno)null);

        var mockLogger = new Mock<ILogger<AlunoController>>();


        var alunoController = new AlunoController(mockAlunoService.Object, mockLogger.Object);

        var id = 1; // Substitua pelo valor desejado

        var result = await alunoController.Get(id);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Post_ReturnsOkObjectResult_WhenAlunoIsAdded()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        var alunoToAdd = new AlunoCursoDTO { Aluno = new Aluno() };
        mockAlunoService.Setup(service => service.AdicionarAlunoAsync(It.IsAny<AlunoCursoDTO>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<AlunoController>>();

        var alunoController = new AlunoController(mockAlunoService.Object, mockLogger.Object);

        var result = await alunoController.Post(alunoToAdd);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(1, (int)okObjectResult.Value);
    }

    [Fact]
    public async Task Put_ReturnsOkResult_WhenAlunoIsUpdated()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        var alunoToUpdate = new Aluno { IdAluno = 2, Nome = "Aluno Atualizado" };
        mockAlunoService.Setup(service => service.AtualizarAlunoAsync(It.IsAny<Aluno>(), It.IsAny<List<int>>()))
               .ReturnsAsync(true);
        var mockLogger = new Mock<ILogger<AlunoController>>();

        var alunoController = new AlunoController(mockAlunoService.Object, mockLogger.Object);

        // Fornecer um valor para o parâmetro 'id'
        var id = 1; // Substitua pelo valor desejado

        var result = await alunoController.Put(id, alunoToUpdate);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True((bool)okResult.Value);
    }


    [Fact]
    public async Task Delete_ReturnsOkResult_WhenAlunoIsDeleted()
    {
        var mockAlunoService = new Mock<IAlunoService>();
        mockAlunoService.Setup(service => service.DeletarAlunoAsync(It.IsAny<int>())).ReturnsAsync(true);

        var mockLogger = new Mock<ILogger<AlunoController>>();

        var alunoController = new AlunoController(mockAlunoService.Object, mockLogger.Object);

        // Fornecer um valor para o parâmetro 'id'
        var id = 1; // Substitua 1 pelo valor desejado

        var result = await alunoController.Delete(id);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True((bool)okResult.Value);
    }
}