using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUniversity.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    [Authorize(Roles = "Aluno")]
    [Consumes("application/json")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private readonly ILogger<AlunoController> _logger;

        public AlunoController(IAlunoService alunoService, ILogger<AlunoController> logger)
        {
            _alunoService = alunoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> Get()
        {
            try
            {
                var alunos = await _alunoService.ObterTodosAlunosAsync();

                _logger.LogInformation("Lista de alunos obtida com sucesso");

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de alunos: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter a lista de alunos");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> Get(int idAluno)
        {
            try
            {
                var aluno = await _alunoService.ObterAlunoPorIdAsync(idAluno);

                if (aluno == null)
                {
                    _logger.LogWarning($"Aluno com ID {idAluno} não encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Aluno com ID {idAluno} encontrado com sucesso");

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter o aluno com ID {idAluno}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter o aluno por ID");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] AlunoCursoDTO alunoCurso)
        {
            try
            {
                if (alunoCurso.Aluno == null)
                {
                    _logger.LogError("Dados do aluno são nulos");
                    return BadRequest("Dados do aluno são nulos");
                }

                var cursoId = await _alunoService.AdicionarAlunoAsync(alunoCurso);

                _logger.LogInformation($"Aluno adicionado com sucesso. ID do curso: {cursoId}");

                return Ok(cursoId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar o aluno: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao adicionar o aluno");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno == null)
                {
                    _logger.LogError("Dados do aluno são nulos");
                    return BadRequest("Dados do aluno são nulos");
                }

                _logger.LogInformation($"Tentativa de atualização do aluno com ID {id}");

                aluno.IdAluno = id;

                // Obtenha a lista de cursos adicionados, se necessário
                var cursosAdicionados = aluno.CursosAlunos?.Select(curso => curso.IdCurso).ToList();

                var resultado = await _alunoService.AtualizarAlunoAsync(aluno, cursosAdicionados);

                if (resultado)
                {
                    _logger.LogInformation($"Aluno com ID {id} atualizado com sucesso");
                    return Ok(true);
                }
                else
                {
                    _logger.LogError($"Falha ao atualizar o aluno com ID {id}");
                    return StatusCode(500, "Falha ao atualizar o aluno");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção durante a atualização do aluno com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro durante a atualização do aluno");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var resultado = await _alunoService.DeletarAlunoAsync(id);

                if (resultado)
                {
                    _logger.LogInformation($"Aluno com ID {id} deletado com sucesso");
                    return Ok(true);
                }
                else
                {
                    _logger.LogError($"Falha ao deletar o aluno com ID {id}");
                    return StatusCode(500, "Falha ao deletar o aluno");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao deletar o aluno com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao deletar o aluno");
            }
        }
    }
}
