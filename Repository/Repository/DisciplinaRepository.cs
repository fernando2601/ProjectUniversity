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

        public async Task<int> AdicionarAsync(DisciplinaDto disciplinaDto)
        {
            try
            {
                _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Mapear a DTO para a entidade Disciplina
                        var disciplina = new Disciplina
                        {
                            Nome = disciplinaDto.Nome,
                            Nota = disciplinaDto.Nota
                        };

                        // Verificar se a DTO possui informações de Curso
                        if (disciplinaDto.Curso != null)
                        {
                            // Mapear a DTO para a entidade Curso
                            var curso = new Curso
                            {
                                Nome = disciplinaDto.Curso.Nome
                                // Mapear outras propriedades, se necessário
                            };

                            // Inserir o Curso no banco de dados e obter o ID gerado
                            var cursoId = await _dbConnection.ExecuteScalarAsync<int>(
                                @"INSERT INTO Curso (Nome)
                          VALUES (@Nome);
                          SELECT CAST(SCOPE_IDENTITY() as int)",
                                new { Nome = curso.Nome },
                                transaction);

                            // Atribuir o ID gerado ao Curso
                            curso.IdCurso = cursoId;

                            // Atribuir o Curso à Disciplina
                            disciplina.Curso = curso;
                        }

                        // Verificar se a DTO possui informações de Professor
                        if (disciplinaDto.Professor != null)
                        {
                            // Mapear a DTO para a entidade Professor
                            var professor = new Professor
                            {
                                Nome = disciplinaDto.Professor.Nome
                                // Mapear outras propriedades, se necessário
                            };

                            // Inserir o Professor no banco de dados e obter o ID gerado
                            var professorId = await _dbConnection.ExecuteScalarAsync<int>(
                                @"INSERT INTO Professor (Nome)
                          VALUES (@Nome);
                          SELECT CAST(SCOPE_IDENTITY() as int)",
                                new { Nome = professor.Nome },
                                transaction);

                            // Atribuir o ID gerado ao Professor
                            professor.IdProfessor = professorId;

                            // Atribuir o Professor à Disciplina
                            disciplina.Professor = professor;
                        }

                        // Inserir a Disciplina no banco de dados e obter o ID gerado
                        var disciplinaId = await _dbConnection.ExecuteScalarAsync<int>(
                            @"INSERT INTO Disciplina (Nome, Nota, IdCurso, IdProfessor)
                      VALUES (@Nome, @Nota, @IdCurso, @IdProfessor);
                      SELECT CAST(SCOPE_IDENTITY() as int)",
                            new
                            {
                                Nome = disciplina.Nome,
                                Nota = disciplina.Nota,
                                IdCurso = disciplina.Curso?.IdCurso, // Pode ser nulo se não houver Curso
                        IdProfessor = disciplina.Professor?.IdProfessor // Pode ser nulo se não houver Professor
                    },
                            transaction);

                        // Atualiza o objeto Disciplina com o ID gerado
                        disciplina.IdDisciplina = disciplinaId;

                        transaction.Commit();

                        return disciplinaId;
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

        public async Task<bool> AtualizarDisciplinaAsync(Disciplina disciplina)
        {
            try
            {
                _dbConnection.Open();

                var result = await _dbConnection.ExecuteAsync(
                    @"UPDATE Disciplina
              SET Nome = @Nome, 
                  Nota = @Nota
              WHERE IdDisciplina = @IdDisciplina",
                    new
                    {
                        Nome = disciplina.Nome,
                        Nota = disciplina.Nota,
                        IdDisciplina = disciplina.IdDisciplina
                    });

                return result > 0;
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





        public async Task<bool> DeletarDisciplinaAsync(int IdDisciplina)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("DELETE FROM Disciplina WHERE IdDisciplina = @IdDisciplina", new { IdDisciplina = IdDisciplina });
            return result > 0;
        }

        public async Task<Disciplina> ObterPorIdAsync(int id)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryFirstOrDefaultAsync<Disciplina>(
                    "SELECT * FROM Disciplina WHERE IdDisciplina = @IdDisciplina",
                    new { IdDisciplina = id }
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


        public async Task<IEnumerable<Disciplina>> ObterTodosAsync()
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryAsync<Disciplina>("SELECT * FROM Disciplina");
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

    }

}