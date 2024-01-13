using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ioc.Extension
{
    public class AuthorizationPolicyConfiguration
    {
        public static void ConfigurePolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                    options.AddPolicy("AlunoProfessor", policy => policy.RequireRole("aluno","professor"));
                    options.AddPolicy("DiretorProfessor", policy => policy.RequireRole("director", "professor"));
                    options.AddPolicy("DiretorAluno", policy => policy.RequireRole("director", "aluno"));
                    options.AddPolicy("Aluno", policy => policy.RequireRole("aluno"));
                    options.AddPolicy("Professor", policy => policy.RequireRole("professor"));
                    options.AddPolicy("Diretor", policy => policy.RequireRole("director"));
                    options.AddPolicy("Disciplina", policy => policy.RequireRole("disciplina"));
                
                });
        }
    }
}
