using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> ObterTodosAsync();
        Task<Aluno> ObterPorIdAsync(Guid id);
        Task<int> AdicionarAsync(Aluno aluno);
        Task<bool> AtualizarAlunoAsync(Aluno aluno);
        Task<bool> DeletarAlunoAsync(Guid id);
    }
}
