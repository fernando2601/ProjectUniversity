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
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> Get()
        {
            var livros = await _cursoService.ObterTodosCursosAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Get(Guid id)
        {
            var curso = await _cursoService.ObterCursoPorIdAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Curso curso)
        {
            var cursoId = await _cursoService.AdicionarCursoAsync(curso);
            return Ok(cursoId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Curso curso)
        {
            curso.IdCurso = id;
            var resultado = await _cursoService.AtualizarCursoAsync(curso);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var resultado = await _cursoService.DeletarCursoAsync(id);
            return Ok(resultado);
        }
    }
}
