using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUsuarioRepository
    {
        Usuario Get(string username, string password);

        void Insert(Usuario usuario);

        void Update(Usuario usuario);


    }
}
