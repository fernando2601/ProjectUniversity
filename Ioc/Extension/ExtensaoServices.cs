using Domain;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interface;
using Repository.Repository;
using Service;
using Service.Interface;
using Service.Service;
using System.Data;
using System.Data.SqlClient;

namespace IOC
{
    public static class ExtensaoServices
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

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();



            services.AddTransient<IDbConnection>(db => new SqlConnection(connectionString));

        }
    }
}