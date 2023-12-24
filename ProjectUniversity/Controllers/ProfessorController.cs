using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> Get()
        {
            var aluno = await _professorService.ObterTodosProfessoresAsync();
            return Ok(aluno);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> Get(int id)
        {
            var aluno = await _professorService.ObterProfessorPorIdAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Professor professor)
        {
            var discipline = await _professorService.AdicionarProfessorAsync(professor);
            return Ok(discipline);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Professor professor)
        {
            professor.IdProfessor = id;
            var resultado = await _professorService.AtualizarProfessorAsync(professor);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var resultado = await _professorService.DeletarProfessorAsync(id);
            return Ok(resultado);
        }
    }
}
