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
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AdicionarAsync(Curso curso)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.ExecuteAsync("INSERT INTO Curso (Semestres, Nome, Disciplina,Professores,Mensalidade) VALUES (@Semestres, @Nome, @Disciplina,@Professores,@Mensalidade); SELECT CAST(SCOPE_IDENTITY() as int)", curso);
        }
        
        public async Task<bool> AtualizarCursoAsync(Curso curso)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteAsync("UPDATE Curso SET Semestres = @Semestres, Nome = @Nome, Mensalidade = @Mensalidade ,Disciplina = @Disciplina, Professores = @Professores WHERE IdCurso = @IdCurso", curso);
            return result > 0;
        }

        public async Task<bool> DeletarCursoAsync(Guid id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteAsync("DELETE FROM Curso WHERE IdCurso = @IdCurso", new { Id = id });
            return result > 0;
        }

        public async Task<Curso> ObterPorIdAsync(Guid id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Curso>("SELECT * FROM Curso WHERE IdCurso = @IdCurso", new { Id = id });
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryAsync<Curso>("SELECT * FROM Curso");
        }
    }
}
