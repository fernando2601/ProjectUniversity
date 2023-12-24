using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> ObterTodosAlunosAsync();
        Task<Aluno> ObterAlunoPorIdAsync(int id);
        Task<int> AdicionarAlunoAsync(AlunoCursoDTO alunoCurso);
        Task<bool> AtualizarAlunoAsync(Aluno aluno, List<int> cursosAdicionados);
        Task<bool> DeletarAlunoAsync(int id);
    }
}
