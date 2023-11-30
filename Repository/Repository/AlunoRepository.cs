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
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnection _dbConnection;

        public AlunoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AdicionarAsync(Aluno aluno)
        {
            _dbConnection.Open();
            return await _dbConnection.ExecuteAsync("INSERT INTO Aluno (Nota, Nome, Idade,Endereço,Mensalidade,Semestre,Disciplina) VALUES (@Nota, @Nome, @Idade,@Endereço,@Mensalidade,@Semestre,@Disciplina); SELECT CAST(SCOPE_IDENTITY() as int)", aluno);
        }

        public async Task<bool> AtualizarAlunoAsync(Aluno aluno)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("UPDATE Aluno SET Nota = @Nota, Nome = @Nome, Idade = @Idade ,Endereço = @Endereço,Mensalidade = @Mensalidade , Semestre = @Semestre ,Disciplina = @Disciplina WHERE Id = @Id", aluno);
            return result > 0;
        }

        public async Task<bool> DeletarAlunoAsync(Guid id)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("DELETE FROM Aluno WHERE Id = @Id", new { Id = id });
            return result > 0;
        }

        public async Task<Aluno> ObterPorIdAsync(Guid id)
        {
            _dbConnection.Open();
            return await _dbConnection.QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM Aluno WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Aluno>("SELECT * FROM Aluno");
        }
    }
}
