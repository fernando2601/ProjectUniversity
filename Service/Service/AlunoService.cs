using Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public  async Task<int> AdicionarAlunoAsync(AlunoCursoDTO alunoCurso)
        {
            return await _alunoRepository.AdicionarAlunoAsync(alunoCurso);
        }

        public Task<bool> AtualizarAlunoAsync(Aluno aluno, List<int> cursosAdicionados)
        {
            return _alunoRepository.AtualizarAlunoAsync(aluno, cursosAdicionados);
        }


        public Task<bool> DeletarAlunoAsync(int id)
        {
            return _alunoRepository.DeletarAlunoAsync(id);
        }

        public Task<Aluno> ObterAlunoPorIdAsync(int id)
        {
            return _alunoRepository.ObterPorIdAsync(id);
        }

        public Task<IEnumerable<Aluno>> ObterTodosAlunosAsync()
        {
            return _alunoRepository.ObterTodosAsync();
        }
    }
}
