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
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _disciplinaService;
        private readonly ILogger<DisciplinaController> _logger;


        public DisciplinaController(IDisciplinaService disciplinaService, ILogger<DisciplinaController> logger)
        {
            _disciplinaService = disciplinaService;
            _logger = logger;

        }
        [HttpGet]
        [Authorize(Policy = "Disciplina")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> ObterTodos()
        {
            try
            {
                var cursos = await _disciplinaService.ObterTodasDisciplinasAsync();

                _logger.LogInformation("Lista de disciplina obtida com sucesso");

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de disciplinas: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter a lista de alunos");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> ObterPorId(int idDisciplina)
        {
            try
            {
                var curso = await _disciplinaService.ObterDisciplinaPorIdAsync(idDisciplina);

                if (curso == null)
                {
                    _logger.LogWarning($"Disciplina com ID {idDisciplina} não encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Disciplina com ID {idDisciplina} encontrado com sucesso");

                return Ok(curso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter o disciplina com ID {idDisciplina}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter o Disciplina por ID");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] DisciplinaDto disciplina)
        {
            try
            {
                if (disciplina == null)
                {
                    _logger.LogError("Dados do disciplina são nulos");
                    return BadRequest("Dados do disciplina são nulos");
                }

                _logger.LogInformation("Tentativa de adicionar um novo disciplina");

                var cursoId = await _disciplinaService.AdicionarDisciplinaoAsync(disciplina);

                _logger.LogInformation($"Novo curso adicionado com ID: {disciplina}");

                return Ok(cursoId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção ao tentar adicionar uma nova disciplina: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao adicionar a nova disciplina");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Disciplina disciplina)
        {
            try
            {
                if (disciplina == null)
                {
                    _logger.LogError("Dados da disciplina são nulos");
                    return BadRequest("Dados da disciplina são nulos");
                }

                _logger.LogInformation($"Tentativa de atualização da disciplina com ID {id}");

                disciplina.IdDisciplina = id;

                var resultado = await _disciplinaService.AtualizarDisciplinaAsync(disciplina);

                if (resultado)
                {
                    _logger.LogInformation($"disciplina com ID {id} atualizado com sucesso");
                    return Ok(true);
                }
                else
                {
                    _logger.LogError($"Falha ao atualizar a disciplina com ID {id}");
                    return StatusCode(500, "Falha ao atualizar a disciplina");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção durante a atualização da disciplina com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro durante a atualização da disciplina");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int IdDisciplina)
        {
            var resultado = await _disciplinaService.DeletarDisciplinaAsync(IdDisciplina);
            return Ok(resultado);
        }
    }
}
