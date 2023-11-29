using Dapper;
using Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly string _connectionString;

        public ProfessorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AdicionarAsync(Professor professor)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.ExecuteAsync("INSERT INTO Professor (Nome,Endereço,Idade,Salário,ProfessorCoordenador) VALUES (@Nome,@Endereço, @Idade,@Salario,@ProfessorCoordenador); SELECT CAST(SCOPE_IDENTITY() as int)", professor);
        }

        public async Task<bool> AtualizarProfessorAsync(Professor professor)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteAsync("UPDATE Professor SET Nome = @Nome,Endereço = @Endereço,Idade = @Idade , Salário = @Salário ,ProfessorCoordenador = @ProfessorCoordenador WHERE IdProfessor = @IdProfessor", professor);
            return result > 0;
        }

        public async Task<bool> DeletarProfessorAsync(Guid id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteAsync("DELETE FROM Professor WHERE IdProfessor = @IdProfessor", new { Id = id });
            return result > 0;
        }

        public async Task<Professor> ObterPorIdAsync(Guid id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Professor>("SELECT * FROM Professor WHERE IdProfessor = @IdProfessor", new { Id = id });
        }

        public async Task<IEnumerable<Professor>> ObterTodosAsync()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryAsync<Professor>("SELECT * FROM Professor");
        }
    }
}
