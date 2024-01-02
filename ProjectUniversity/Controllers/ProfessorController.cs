using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectUniversity.Controllers
{
    [ApiController]
    [Route("api/professor")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;
        private readonly ILogger<ProfessorController> _logger;

        public ProfessorController(IProfessorService professorService, ILogger<ProfessorController> logger)
        {
            _professorService = professorService;
            _logger = logger;

        }
        [HttpGet]
        [Authorize(Roles = "Professor,professor")]
        public async Task<ActionResult<IEnumerable<Professor>>> Get()
        {
            try
            {
                var cursos = await _professorService.ObterTodosProfessoresAsync();

                _logger.LogInformation("Lista de professores obtida com sucesso");

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de professores: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter a lista de professores");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "professor")]

        public async Task<ActionResult<Professor>> Get(int id)
        {
            try
            {
                var curso = await _professorService.ObterProfessorPorIdAsync(id);

                if (curso == null)
                {
                    _logger.LogWarning($"Professor com ID {id} não encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Professor com ID {id} encontrado com sucesso");

                return Ok(curso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter o professor com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter o professor por ID");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Professor professor)
        {
            try
            {
                if (professor == null)
                {
                    _logger.LogError("Dados do professor são nulos");
                    return BadRequest("Dados do professor são nulos");
                }

                _logger.LogInformation("Tentativa de adicionar um novo curso");

                var cursoId = await _professorService.AdicionarProfessorAsync(professor);

                _logger.LogInformation($"Novo professor adicionado com ID: {cursoId}");

                return Ok(cursoId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção ao tentar adicionar um novo professor: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao adicionar o novo professor");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Professor professor)
        {
            try
            {
                if (professor == null)
                {
                    _logger.LogError("Dados do professor são nulos");
                    return BadRequest("Dados do professor são nulos");
                }

                _logger.LogInformation($"Tentativa de atualização do professor com ID {id}");

                professor.IdProfessor = id;

                var resultado = await _professorService.AtualizarProfessorAsync(professor);

                if (resultado)
                {
                    _logger.LogInformation($"Professor com ID {id} atualizado com sucesso");
                    return Ok(true);
                }
                else
                {
                    _logger.LogError($"Falha ao atualizar o professor com ID {id}");
                    return StatusCode(500, "Falha ao atualizar o professor");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção durante a atualização do professor com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro durante a atualização do professor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _professorService.DeletarProfessorAsync(id);

                if (result)
                {
                    return NoContent(); // Retorna 204 No Content se a exclusão for bem-sucedida
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o professor não for encontrado
                }
            }
            catch (Exception ex)
            {
                // Log de exceção, etc.
                return StatusCode(500, "Erro interno do servidor");
            }
        }

    }
}
