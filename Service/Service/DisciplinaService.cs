using Domain;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly string _connectionString;

        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaService(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        public Task<int> AdicionarDisciplinaoAsync(Disciplina disciplina)
        {
            return _disciplinaRepository.AdicionarAsync(disciplina);
        }

        public Task<bool> AtualizarDisciplinaAsync(Disciplina disciplina)
        {
            return _disciplinaRepository.AtualizarDisciplinaAsync(disciplina);
        }

        public Task<bool> DeletarDisciplinaAsync(int id)
        {
            return _disciplinaRepository.DeletarDisciplinaAsync(id);
        }

        public Task<Disciplina> ObterDisciplinaPorIdAsync(int id)
        {
            return _disciplinaRepository.ObterPorIdAsync(id);
        }

        public Task<IEnumerable<Disciplina>> ObterTodasDisciplinasAsync()
        {
            return _disciplinaRepository.ObterTodosAsync();
        }
    }
}
