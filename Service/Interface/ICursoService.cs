using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICursoService
    {
        Task<IEnumerable<Curso>> ObterTodosCursosAsync();
        Task<Curso> ObterCursoPorIdAsync(int id);
        Task<int> AdicionarCursoAsync(Curso curso);
        Task<bool> AtualizarCursoAsync(Curso curso);
        Task<bool> DeletarCursoAsync(int id);
    }
}
