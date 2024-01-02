using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDiretorService
    {
        Task<IEnumerable<Diretor>> ObterTodosDiretoresAsync();
        Task<Diretor> ObterDiretorPorIdAsync(int id);
        Task<int> AdicionarDiretorAsync(Diretor professor);
        Task<bool> AtualizarDiretorAsync(Diretor professor);
        Task<bool> DeletarDiretorAsync(int id);
    }
}
