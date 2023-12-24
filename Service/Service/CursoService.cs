using Domain;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public Task<int> AdicionarCursoAsync(Curso curso)
        {
            return _cursoRepository.AdicionarAsync(curso);

        }

        public Task<bool> AtualizarCursoAsync(Curso curso)
        {
            return _cursoRepository.AtualizarCursoAsync(curso);
        }

        public Task<bool> DeletarCursoAsync(int id)
        {
            return _cursoRepository.DeletarCursoAsync(id);
        }

        public Task<Curso> ObterCursoPorIdAsync(int id)
        {
            return _cursoRepository.ObterPorIdAsync(id);
        }

        public Task<IEnumerable<Curso>> ObterTodosCursosAsync()
        {
            return _cursoRepository.ObterTodosAsync();
        }
    }
}
