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
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinaController(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> Get()
        {
            var aluno = await _disciplinaService.ObterTodasDisciplinasAsync();
            return Ok(aluno);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> Get(Guid id)
        {
            var aluno = await _disciplinaService.ObterDisciplinaPorIdAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Disciplina disciplina)
        {
            var discipline = await _disciplinaService.AdicionarDisciplinaoAsync(disciplina);
            return Ok(discipline);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Disciplina disciplina)
        {
            disciplina.Id = id;
            var resultado = await _disciplinaService.AtualizarDisciplinaAsync(disciplina);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var resultado = await _disciplinaService.DeletarDisciplinaAsync(id);
            return Ok(resultado);
        }
    }
}
