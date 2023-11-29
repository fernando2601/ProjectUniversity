using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDisciplinaRepository
    {
        Task<IEnumerable<Disciplina>> ObterTodosAsync();
        Task<Disciplina> ObterPorIdAsync(Guid id);
        Task<int> AdicionarAsync(Disciplina disciplina);
        Task<bool> AtualizarDisciplinaAsync(Disciplina disciplina);
        Task<bool> DeletarDisciplinaAsync(Guid id);
    }
}
