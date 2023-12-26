using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProjectUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly ILogger<CursoController> _logger;


        public CursoController(ICursoService cursoService, ILogger<CursoController> logger)
        {
            _cursoService = cursoService;
            _logger = logger;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> ObterTodos()
        {
            try
            {
                var cursos = await _cursoService.ObterTodosCursosAsync();

                _logger.LogInformation("Lista de cursos obtida com sucesso");

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de cursos: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter a lista de alunos");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> ObterCursoPorId(int idCurso)
        {
            try
            {
                var curso = await _cursoService.ObterCursoPorIdAsync(idCurso);

                if (curso == null)
                {
                    _logger.LogWarning($"Curso com ID {idCurso} não encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Curso com ID {idCurso} encontrado com sucesso");

                return Ok(curso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter o curso com ID {idCurso}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter o curso por ID");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Curso curso)
        {
            try
            {
                if (curso == null)
                {
                    _logger.LogError("Dados do curso são nulos");
                    return BadRequest("Dados do curso são nulos");
                }

                _logger.LogInformation("Tentativa de adicionar um novo curso");

                var cursoId = await _cursoService.AdicionarCursoAsync(curso);

                _logger.LogInformation($"Novo curso adicionado com ID: {cursoId}");

                return Ok(cursoId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção ao tentar adicionar um novo curso: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao adicionar o novo curso");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Curso curso)
        {
            try
            {
                if (curso == null)
                {
                    _logger.LogError("Dados do curso são nulos");
                    return BadRequest("Dados do curso são nulos");
                }

                _logger.LogInformation($"Tentativa de atualização do curso com ID {id}");

                curso.IdCurso = id;

                var resultado = await _cursoService.AtualizarCursoAsync(curso);

                if (resultado)
                {
                    _logger.LogInformation($"Curso com ID {id} atualizado com sucesso");
                    return Ok(true);
                }
                else
                {
                    _logger.LogError($"Falha ao atualizar o curso com ID {id}");
                    return StatusCode(500, "Falha ao atualizar o curso");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção durante a atualização do curso com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro durante a atualização do curso");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var resultado = await _cursoService.DeletarCursoAsync(id);
            return Ok(resultado);
        }
    }
}
