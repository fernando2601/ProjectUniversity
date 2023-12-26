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
        private readonly IDisciplinaRepository _disciplinaRepository;


        public ProfessorRepository(IDbConnection dbConnection, IDisciplinaRepository disciplinaRepository)
        {
            _dbConnection = dbConnection;
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task<int> AdicionarAsync(Professor professor)
        {
            try
            {
                _dbConnection.Open();

                return await _dbConnection.ExecuteAsync(
                    @"INSERT INTO Professor (Nome, Endereco, Idade, Salario, ProfessorCoordenador)
              VALUES (@Nome, @Endereco, @Idade, @Salario, @ProfessorCoordenador);
              SELECT CAST(SCOPE_IDENTITY() as int)",
                    professor
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


        public async Task<bool> AtualizarProfessorAsync(Professor professor)
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
                        var professorUpdateQuery = "UPDATE Professor SET Nome = @Nome, Endereco = @Endereco, " +
                                                   "Idade = @Idade, Salario = @Salario, ProfessorCoordenador = @ProfessorCoordenador " +
                                                   "WHERE IdProfessor = @IdProfessor";

                        var resultProfessor = await _dbConnection.ExecuteAsync(professorUpdateQuery, professor, transaction);

                        transaction.Commit();

                        return resultProfessor > 0;
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

        public async Task<IEnumerable<DisciplinaProfessorDto>> ObterDisciplinasPorProfessorAsync(int idProfessor)
        {
            try
            {
                string query = @"
            SELECT D.IdDisciplina, D.Nota, D.Nome AS DisciplinaNome, 
                   C.IdCurso, C.Semestres, C.Nome AS CursoNome
            FROM Disciplina D
            INNER JOIN Curso C ON D.IdCurso = C.IdCurso
            WHERE D.IdProfessor = @IdProfessor";

                var disciplinas = await _dbConnection.QueryAsync<DisciplinaProfessorDto>(query, new { IdProfessor = idProfessor });

                return disciplinas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> DeletarProfessorAsync(int idProfessor)
        {
            try
            {
                // Obter as disciplinas associadas ao professor
                var disciplinas = await ObterDisciplinasPorProfessorAsync(idProfessor);

                // Excluir as disciplinas
                foreach (var disciplina in disciplinas)
                {
                    await _dbConnection.ExecuteAsync("DELETE FROM Disciplina WHERE IdDisciplina = @IdDisciplina", new { IdDisciplina = disciplina.IdDisciplina });
                }

                // Excluir o professor
                var resultProfessor = await _dbConnection.ExecuteAsync("DELETE FROM Professor WHERE IdProfessor = @IdProfessor", new { IdProfessor = idProfessor });

                return resultProfessor > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Professor> ObterPorIdAsync(int id)
        {
            _dbConnection.Open();
            try
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<Professor>("SELECT * FROM Professor WHERE IdProfessor = @IdProfessor", new { IdProfessor = id });
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<Professor>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Professor>("SELECT * FROM Professor");
        }
    }
}
