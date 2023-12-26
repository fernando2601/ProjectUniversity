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
            try
            {
                _dbConnection.Open(); 

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        var cursoId = await _dbConnection.ExecuteScalarAsync<int>(
                            @"INSERT INTO Curso (Semestres, Nome, Mensalidade)
                      VALUES (@Semestres, @Nome, @Mensalidade);
                      SELECT CAST(SCOPE_IDENTITY() as int)",
                            new
                            {
                                Semestres = curso.Semestres,
                                Nome = curso.Nome,
                                Mensalidade = curso.Mensalidade
                            }, transaction);

                        // Atualiza o objeto Curso com o ID gerado
                        curso.IdCurso = cursoId;

                        transaction.Commit();

                        return cursoId;
                    }
                    catch
                    {
                        // Rollback em caso de erro
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _dbConnection.Close();
            }
        }


        public async Task<bool> AtualizarCursoAsync(Curso curso)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var cursoUpdateQuery = "UPDATE Curso SET Semestres = @Semestres, Nome = @Nome, " +
                                           "Mensalidade = @Mensalidade WHERE IdCurso = @IdCurso";
                    var resultCurso = await _dbConnection.ExecuteAsync(cursoUpdateQuery, curso, transaction);

                    transaction.Commit();

                    return resultCurso > 0;
                }
                catch
                {

                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<bool> DeletarCursoAsync(int id)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("DELETE FROM Curso WHERE IdCurso = @IdCurso", new { IdCurso = id });
            return result > 0;
        }

        public async Task<Curso> ObterPorIdAsync(int idCurso)
        {
            _dbConnection.Open();

            try
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<Curso>(
                    "SELECT * FROM Curso WHERE IdCurso = @IdCurso",
                    new { IdCurso = idCurso });
            }
            finally
            {
                _dbConnection.Close();
            }
        }
   

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Curso>("SELECT * FROM Curso");
        }
    }
}
