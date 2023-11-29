using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProfessorService
    {
        Task<IEnumerable<Professor>> ObterTodosProfessoresAsync();
        Task<Professor> ObterProfessorPorIdAsync(Guid id);
        Task<int> AdicionarProfessorAsync(Professor professor);
        Task<bool> AtualizarProfessorAsync(Professor professor);
        Task<bool> DeletarProfessorAsync(Guid id);
    }
}
