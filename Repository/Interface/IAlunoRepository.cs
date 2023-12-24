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
        Task<Aluno> ObterPorIdAsync(int id);
        Task<int> AdicionarAlunoAsync(AlunoCursoDTO alunoCurso);
        Task<bool> AtualizarAlunoAsync(Aluno aluno, List<int> cursosAdicionados);
        Task<bool> DeletarAlunoAsync(int id);
    }
}
