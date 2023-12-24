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
        public async Task<int> AdicionarAlunoAsync(AlunoCursoDTO alunoCurso)
        {
            try
            {
                _dbConnection.Open();  // Abre a conexão de forma síncrona

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Adicionar aluno e obter o ID gerado automaticamente
                        var alunoId = await _dbConnection.ExecuteScalarAsync<int>(
                            @"INSERT INTO Aluno (Nota, Nome, Idade, Endereco, Mensalidade, Semestre)
                      VALUES (@Nota, @Nome, @Idade, @Endereco, @Mensalidade, @Semestre);
                      SELECT SCOPE_IDENTITY()",
                            new
                            {
                                alunoCurso.Aluno.IdAluno,  // Certifique-se de que IdAluno seja definido corretamente
                                alunoCurso.Aluno.Nota,
                                alunoCurso.Aluno.Nome,
                                alunoCurso.Aluno.Idade,
                                alunoCurso.Aluno.Endereco,
                                alunoCurso.Aluno.Mensalidade,
                                alunoCurso.Aluno.Semestre
                            }, transaction);

                        // Adicionar matrícula do aluno no curso
                        await _dbConnection.ExecuteAsync(
    @"INSERT INTO AlunoCursoDTO (IdAluno, IdCurso, DataMatricula, StatusMatricula)
      VALUES (@IdAluno, @IdCurso, GETDATE(), @StatusMatricula)",
    new
    {
        IdAluno = alunoId,
        alunoCurso.IdCurso,
        StatusMatricula = alunoCurso.StatusMatricula
    }, transaction);

                        // Commit da transação se todas as operações forem bem-sucedidas
                        transaction.Commit();

                        return alunoId;
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
                // Adicione mensagens de log para rastrear o erro
                Console.WriteLine($"Ocorreu um erro ao adicionar o aluno: {ex.Message}");
                throw; // Re-throw a exceção para que ela seja tratada no nível superior, se necessário.
            }
            finally
            {
                _dbConnection.Close();  // Fecha a conexão no final
            }
        }



        public async Task<bool> AtualizarAlunoAsync(Aluno aluno, List<int> cursosAdicionados)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Atualiza os dados do aluno na tabela Aluno
                    var alunoUpdateQuery = "UPDATE Aluno SET Nota = @Nota, Nome = @Nome, Idade = @Idade, " +
                                           "Endereco = @Endereco, Mensalidade = @Mensalidade, Semestre = @Semestre " +
                                           "WHERE IdAluno = @IdAluno"; // Corrigido para usar IdAluno
                    var resultAluno = await _dbConnection.ExecuteAsync(alunoUpdateQuery, aluno, transaction);

                    // Adiciona cursos na tabela AlunoCursoDto
                    foreach (var cursoId in cursosAdicionados)
                    {
                        var cursoInsertQuery = "INSERT INTO AlunoCursoDto (IdAluno, IdCurso) VALUES (@AlunoId, @CursoId)";
                        await _dbConnection.ExecuteAsync(cursoInsertQuery, new { AlunoId = aluno.IdAluno, CursoId = cursoId }, transaction);
                    }

                    // Commit da transação se todas as operações forem bem-sucedidas
                    transaction.Commit();

                    // Retorna verdadeiro se alguma linha foi afetada na tabela Aluno
                    return resultAluno > 0;
                }
                catch
                {
                    // Rollback em caso de erro
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }


        public async Task<bool> DeletarAlunoAsync(int id)
        {
            _dbConnection.Open();
            var result = await _dbConnection.ExecuteAsync("DELETE FROM Aluno WHERE IdAluno = @IdAluno", new { IdAluno = id });
            return result > 0;
        }


        public async Task<Aluno> ObterPorIdAsync(int idAluno)
        {
            _dbConnection.Open();

            try
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<Aluno>(
                    "SELECT * FROM Aluno WHERE IdAluno = @IdAluno",
                    new { IdAluno = idAluno });
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            _dbConnection.Open();
            return await _dbConnection.QueryAsync<Aluno>("SELECT * FROM Aluno");
        }
    }
}
