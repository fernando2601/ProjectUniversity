using Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDiretorRepository
    {        
            Task<IEnumerable<Diretor>> ObterTodosAsync();
            Task<Diretor> ObterPorIdAsync(int id);
            Task<int> AdicionarAsync(Diretor diretor);
            Task<bool> AtualizarDiretorAsync(Diretor diretor);
            Task<bool> DeletarDiretorAsync(int id);
        
    }
}
