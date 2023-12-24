using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> ObterTodosAsync();
        Task<Curso> ObterPorIdAsync(int id);
        Task<int> AdicionarAsync(Curso curso);
        Task<bool> AtualizarCursoAsync(Curso curso);
        Task<bool> DeletarCursoAsync(int id);
    }
}
