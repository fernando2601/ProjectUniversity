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
        private readonly IDbConnection _dbConnection;

        public ProfessorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AdicionarAsync(Professor professor)
        {
            _dbConnection.Open();
            return await _dbConnection.ExecuteAsync("INSERT INTO Professor (Nome,Endereço,Idade,Salário,ProfessorCoordenador) VALUES (@Nome,@Endereço, @Idade,@Salario,@ProfessorCoordenador); SELECT CAST(SCOPE_IDENTITY() as int)", professor);
        }

        public async Task<bool> AtualizarProfessorAsync(Professor professor)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("UPDATE Professor SET Nome = @Nome,Endereço = @Endereço,Idade = @Idade , Salário = @Salário ,ProfessorCoordenador = @ProfessorCoordenador WHERE IdProfessor = @IdProfessor", professor);
            return result > 0;
        }

        public async Task<bool> DeletarProfessorAsync(int id)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("DELETE FROM Professor WHERE IdProfessor = @IdProfessor", new { Id = id });
            return result > 0;
        }

        public async Task<Professor> ObterPorIdAsync(int id)
        {
            _dbConnection.Open();
            return await _dbConnection.QueryFirstOrDefaultAsync<Professor>("SELECT * FROM Professor WHERE IdProfessor = @IdProfessor", new { Id = id });
        }

        public async Task<IEnumerable<Professor>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Professor>("SELECT * FROM Professor");
        }
    }
}
