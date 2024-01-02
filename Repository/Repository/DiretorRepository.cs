using Dapper;
using Domain;
using Domain.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository
{
    public class DiretorRepository : IDiretorRepository
    {
        private readonly IDbConnection _dbConnection;

        public DiretorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AdicionarAsync(Diretor diretor)
        {
            try
            {
                _dbConnection.Open();

                return await _dbConnection.ExecuteAsync(
                    @"INSERT INTO Diretor (Nome, Idade, Salario, Endereco)
                      VALUES (@Nome, @Idade, @Salario, @Endereco);
                      SELECT CAST(SCOPE_IDENTITY() as int)",
                    diretor
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<bool> AtualizarDiretorAsync(Diretor diretor)
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection.Open();
                }

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        var diretorUpdateQuery = "UPDATE Diretor SET Nome = @Nome, Idade = @Idade, " +
                                                "Salario = @Salario, Endereco = @Endereco " +
                                                "WHERE IdDiretor = @IdDiretor";

                        var resultDiretor = await _dbConnection.ExecuteAsync(diretorUpdateQuery, diretor, transaction);

                        transaction.Commit();

                        return resultDiretor > 0;
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Diretor>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Diretor>("SELECT * FROM Diretor");
        }

        public async Task<Diretor> ObterPorIdAsync(int id)
        {
            _dbConnection.Open();
            try
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<Diretor>("SELECT * FROM Diretor WHERE IdDiretor = @IdDiretor", new { IdDiretor = id });
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<bool> DeletarDiretorAsync(int idDiretor)
        {
            try
            {
                var resultDiretor = await _dbConnection.ExecuteAsync("DELETE FROM Diretor WHERE IdDiretor = @IdDiretor", new { IdDiretor = idDiretor });

                return resultDiretor > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
