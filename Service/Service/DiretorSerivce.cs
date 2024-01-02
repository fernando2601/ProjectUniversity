using Domain;
using Domain.Domain;
using Repository.Interface;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DiretorService : IDiretorService
    {
        private readonly IDiretorRepository _diretorRepository;

        public DiretorService(IDiretorRepository diretorRepository)
        {
            _diretorRepository = diretorRepository;
        }

        public Task<int> AdicionarDiretorAsync(Diretor diretor)
        {
            return _diretorRepository.AdicionarAsync(diretor);
        }

        public Task<bool> AtualizarDiretorAsync(Diretor diretor)
        {
            return _diretorRepository.AtualizarDiretorAsync(diretor);
        }

        public Task<bool> DeletarDiretorAsync(int id)
        {
            return _diretorRepository.DeletarDiretorAsync(id);
        }

        public Task<Diretor> ObterDiretorPorIdAsync(int id)
        {
            return _diretorRepository.ObterPorIdAsync(id);
        }

        public Task<IEnumerable<Diretor>> ObterTodosDiretoresAsync()
        {
            return _diretorRepository.ObterTodosAsync();
        }
    }
}
