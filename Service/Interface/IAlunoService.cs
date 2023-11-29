using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> ObterTodosAlunosAsync();
        Task<Aluno> ObterAlunoPorIdAsync(Guid id);
        Task<int> AdicionarAlunoAsync(Aluno aluno);
        Task<bool> AtualizarAlunoAsync(Aluno aluno);
        Task<bool> DeletarAlunoAsync(Guid id);
    }
}
