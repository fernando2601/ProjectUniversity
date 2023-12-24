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
    public class ProfessorService : IProfessorService
    {
        private readonly string _connectionString;

        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public Task<int> AdicionarProfessorAsync(Professor professor)
        {
            return _professorRepository.AdicionarAsync(professor);
        }

        public Task<bool> AtualizarProfessorAsync(Professor professor)
        {
            return _professorRepository.AtualizarProfessorAsync(professor);
        }

        public Task<bool> DeletarProfessorAsync(int id)
        {
            return _professorRepository.DeletarProfessorAsync(id);
        }

        public Task<Professor> ObterProfessorPorIdAsync(int id)
        {
            return _professorRepository.ObterPorIdAsync(id);
        }

        public Task<IEnumerable<Professor>> ObterTodosProfessoresAsync()
        {
            return _professorRepository.ObterTodosAsync();
        }
    }
}
