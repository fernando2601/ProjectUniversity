using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<Professor>> ObterTodosAsync();
        Task<Professor> ObterPorIdAsync(Guid id);
        Task<int> AdicionarAsync(Professor professor);
        Task<bool> AtualizarProfessorAsync(Professor professor);
        Task<bool> DeletarProfessorAsync(Guid id);
    }
}
