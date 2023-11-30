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
        private readonly IDbConnection _dbConnection;

        public CursoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AdicionarAsync(Curso curso)
        {
            _dbConnection.Open();
            return await _dbConnection.ExecuteAsync("INSERT INTO Curso (Semestres, Nome, Disciplina,Professores,Mensalidade) VALUES (@Semestres, @Nome, @Disciplina,@Professores,@Mensalidade); SELECT CAST(SCOPE_IDENTITY() as int)", curso);
        }
        
        public async Task<bool> AtualizarCursoAsync(Curso curso)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("UPDATE Curso SET Semestres = @Semestres, Nome = @Nome, Mensalidade = @Mensalidade ,Disciplina = @Disciplina, Professores = @Professores WHERE IdCurso = @IdCurso", curso);
            return result > 0;
        }

        public async Task<bool> DeletarCursoAsync(Guid id)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("DELETE FROM Curso WHERE IdCurso = @IdCurso", new { Id = id });
            return result > 0;
        }

        public async Task<Curso> ObterPorIdAsync(Guid id)
        {
            _dbConnection.Open();
            return await _dbConnection.QueryFirstOrDefaultAsync<Curso>("SELECT * FROM Curso WHERE IdCurso = @IdCurso", new { Id = id });
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Curso>("SELECT * FROM Curso");
        }
    }
}
