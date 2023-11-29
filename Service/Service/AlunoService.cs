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

        public  Task<int> AdicionarAlunoAsync(Aluno aluno)
        {
            return _alunoRepository.AdicionarAsync(aluno);
        }

        public Task<bool> AtualizarAlunoAsync(Aluno aluno)
        {
            return _alunoRepository.AtualizarAlunoAsync(aluno);
        }

        public Task<bool> DeletarAlunoAsync(Guid id)
        {
            return _alunoRepository.DeletarAlunoAsync(id);
        }

        public Task<Aluno> ObterAlunoPorIdAsync(Guid id)
        {
            return _alunoRepository.ObterPorIdAsync(id);
        }

        public Task<IEnumerable<Aluno>> ObterTodosAlunosAsync()
        {
            return _alunoRepository.ObterTodosAsync();
        }
    }
}
