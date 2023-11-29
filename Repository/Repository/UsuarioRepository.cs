using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public  class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Usuario Get(Usuario usuario ,string username, string password)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                return dbConnection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Username = @Username AND Password = @Password", usuario);
            }
        }

        public  void Insert(Usuario usuario)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                dbConnection.Execute("INSERT INTO Usuarios (Username, Password, Role) VALUES (@Username, @Password, @Role)", usuario);
            }
        }

        public  void Update(Usuario usuario)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                dbConnection.Execute("UPDATE Usuarios SET Username = @Username, Password = @Password, Role = @Role WHERE Id = @Id", usuario);
            }

        }
    }
}