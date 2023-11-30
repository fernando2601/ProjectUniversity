using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interface;
using Service;
using Service.Interface;
using Service.Service;
using System.Data;
using System.Data.SqlClient;

namespace IOC
{
    public static class ExtensaoServicos
    {
        public static void AdicionarServicos(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAlunoRepository, AlunoRepository>();
            services.AddTransient<IAlunoService, AlunoService>();

            services.AddTransient<ICursoRepository, CursoRepository>();
            services.AddTransient<ICursoService, CursoService>();

            services.AddTransient<IDisciplinaRepository, DisciplinaRepository>();
            services.AddTransient<IDisciplinaService, DisciplinaService>();

            services.AddTransient<IProfessorRepository, ProfessorRepository>();
            services.AddTransient<IProfessorService, ProfessorService>();

            services.AddTransient<IDbConnection>(db => new SqlConnection(connectionString));
        }
    }
}