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
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly string _connectionString;

        public DisciplinaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AdicionarAsync(Disciplina disciplina)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.ExecuteAsync("INSERT INTO Curso (Nome, Alunos,Professores) VALUES (@Nome, @Alunos,@Professores); SELECT CAST(SCOPE_IDENTITY() as int)", disciplina);
        }

        public async Task<bool> AtualizarDisciplinaAsync(Disciplina disciplina)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteAsync("UPDATE Curso SET Nome = @Nome, Professores = @Professores , Alunos = @Alunos WHERE Id = @Id", disciplina);
            return result > 0;
        }

        public async Task<bool> DeletarDisciplinaAsync(Guid id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteAsync("DELETE FROM Disciplina WHERE Id = @Id", new { Id = id });
            return result > 0;
        }

        public async Task<Disciplina> ObterPorIdAsync(Guid id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Disciplina>("SELECT * FROM Disciplina WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Disciplina>> ObterTodosAsync()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryAsync<Disciplina>("SELECT * FROM Disciplina");
        }
    }

}