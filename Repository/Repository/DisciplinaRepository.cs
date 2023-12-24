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
        private readonly IDbConnection _dbConnection;

        public DisciplinaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AdicionarAsync(Disciplina disciplina)
        {
            _dbConnection.Open();
            return await _dbConnection.ExecuteAsync("INSERT INTO Curso (Nome, Alunos,Professores) VALUES (@Nome, @Alunos,@Professores); SELECT CAST(SCOPE_IDENTITY() as int)", disciplina);
        }

        public async Task<bool> AtualizarDisciplinaAsync(Disciplina disciplina)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("UPDATE Curso SET Nome = @Nome, Professores = @Professores , Alunos = @Alunos WHERE Id = @Id", disciplina);
            return result > 0;
        }

        public async Task<bool> DeletarDisciplinaAsync(int id)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("DELETE FROM Disciplina WHERE Id = @Id", new { Id = id });
            return result > 0;
        }

        public async Task<Disciplina> ObterPorIdAsync(int id)
        {
            _dbConnection.Open();
            return await _dbConnection.QueryFirstOrDefaultAsync<Disciplina>("SELECT * FROM Disciplina WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Disciplina>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Disciplina>("SELECT * FROM Disciplina");
        }
    }

}