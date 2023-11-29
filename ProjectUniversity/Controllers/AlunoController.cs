using Domain;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> Get()
        {
            var aluno = await _alunoService.ObterTodosAlunosAsync();
            return Ok(aluno);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> Get(Guid id)
        {
            var aluno = await _alunoService.ObterAlunoPorIdAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Aluno aluno)
        {
            var cursoId = await _alunoService.AdicionarAlunoAsync(aluno);
            return Ok(cursoId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Aluno aluno)
        {
            aluno.Id = id;
            var resultado = await _alunoService.AtualizarAlunoAsync(aluno);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var resultado = await _alunoService.DeletarAlunoAsync(id);
            return Ok(resultado);
        }
    }
}
