using Dapper;
using Domain;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class UsuarioRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public static Usuario Get(Usuario usuario, string username, string password)
        {
            using (IDbConnection dbConnection = new SqlConnection())
            {
                dbConnection.Open();

                return dbConnection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Username = @Username AND Password = @Password", usuario);
            }
        }
        public void Insert(Usuario usuario)
        {
            _dbConnection.Open();

            _dbConnection.Execute("INSERT INTO Usuarios (Username, Password, Role) VALUES (@Username, @Password, @Role)", usuario);
        }

        public void Update(Usuario usuario)
        {
            _dbConnection.Open();

            _dbConnection.Execute("UPDATE Usuarios SET Username = @Username, Password = @Password, Role = @Role WHERE Id = @Id", usuario);
        }
    }
}