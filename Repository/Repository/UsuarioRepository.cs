using Dapper;
using Domain;
using Repository.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Usuario Get(string username, string password)
        {
            try
            {
                _dbConnection.Open();

                return _dbConnection.QueryFirstOrDefault<Usuario>(
                    "SELECT * FROM Usuario WHERE Username = @Username AND Password = @Password",
                    new { Username = username, Password = password }
                );
            }
            catch (Exception ex)
            {
                // Lidere com a exceção aqui, você pode registrar, relatar ou lançar novamente se necessário.
                Console.WriteLine($"Erro ao obter usuário: {ex.Message}");
                throw;
            }
            finally
            {
                _dbConnection.Close();
            }
        }


        public void Insert(Usuario usuario)
        {
            _dbConnection.Open();

            _dbConnection.Execute("INSERT INTO Usuario (Username, Password, Role) VALUES (@Username, @Password, @Role)", usuario);
        }

        public void Update(Usuario usuario)
        {
            _dbConnection.Open();

            _dbConnection.Execute("UPDATE Usuario SET Username = @Username, Password = @Password, Role = @Role WHERE Id = @Id", usuario);
        }
    }
}