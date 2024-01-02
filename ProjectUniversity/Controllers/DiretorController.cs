using Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectUniversity.Controllers
{
    [ApiController]
    [Route("api/diretor")]
    public class DiretorController : ControllerBase
    {
        private readonly IDiretorService _diretorService;
        private readonly ILogger<DiretorController> _logger;

        public DiretorController(IDiretorService diretorService, ILogger<DiretorController> logger)
        {
            _diretorService = diretorService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Diretor")]
        public async Task<ActionResult<IEnumerable<Diretor>>> Get()
        {
            try
            {
                var diretores = await _diretorService.ObterTodosDiretoresAsync();

                _logger.LogInformation("Lista de diretores obtida com sucesso");

                return Ok(diretores);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de diretores: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter a lista de diretores");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Diretor")]
        public async Task<ActionResult<Diretor>> Get(int id)
        {
            try
            {
                var diretor = await _diretorService.ObterDiretorPorIdAsync(id);

                if (diretor == null)
                {
                    _logger.LogWarning($"Diretor com ID {id} não encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Diretor com ID {id} encontrado com sucesso");

                return Ok(diretor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter o diretor com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao obter o diretor por ID");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Diretor")]
        public async Task<ActionResult<int>> Post([FromBody] Diretor diretor)
        {
            try
            {
                if (diretor == null)
                {
                    _logger.LogError("Dados do diretor são nulos");
                    return BadRequest("Dados do diretor são nulos");
                }

                _logger.LogInformation("Tentativa de adicionar um novo diretor");

                var diretorId = await _diretorService.AdicionarDiretorAsync(diretor);

                _logger.LogInformation($"Novo diretor adicionado com ID: {diretorId}");

                return Ok(diretorId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção ao tentar adicionar um novo diretor: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao adicionar o novo diretor");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Diretor")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Diretor diretor)
        {
            try
            {
                if (diretor == null)
                {
                    _logger.LogError("Dados do diretor são nulos");
                    return BadRequest("Dados do diretor são nulos");
                }

                _logger.LogInformation($"Tentativa de atualização do diretor com ID {id}");

                diretor.IdDiretor = id;

                var resultado = await _diretorService.AtualizarDiretorAsync(diretor);

                if (resultado)
                {
                    _logger.LogInformation($"Diretor com ID {id} atualizado com sucesso");
                    return Ok(true);
                }
                else
                {
                    _logger.LogError($"Falha ao atualizar o diretor com ID {id}");
                    return StatusCode(500, "Falha ao atualizar o diretor");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção durante a atualização do diretor com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro durante a atualização do diretor");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Diretor")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _diretorService.DeletarDiretorAsync(id);

                if (result)
                {
                    return NoContent(); // Retorna 204 No Content se a exclusão for bem-sucedida
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o diretor não for encontrado
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
