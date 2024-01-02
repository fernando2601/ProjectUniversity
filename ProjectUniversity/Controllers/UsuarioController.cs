using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using Service.Interface;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly JwtSettings _jwtSettings;

        public UsuarioController(IUsuarioService usuarioService, JwtSettings jwtSettings)
        {
            _usuarioService = usuarioService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuario usuario)
        {
            try
            {
                // Chamar o serviço para autenticação
                var user = _usuarioService.Authenticate(usuario.Username, usuario.Password);

                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(user, _jwtSettings);
                user.Password = "";

                Console.WriteLine($"Reivindicações do usuário: {string.Join(", ", (User.Identity as ClaimsIdentity)?.Claims?.Select(c => $"{c.Type}={c.Value}"))}");


                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na autenticação: {ex.Message}");
                return StatusCode(500, new { message = "Erro interno do servidor" });
            }
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("aluno-authenticated")]
        //[Authorize(Policy = "Aluno")]
        public string AlunoAuthenticated()
        {
            // Verificar se o usuário autenticado é um aluno
            if (IsAuthenticatedAluno(User))
            {
                return "Autenticado como Aluno";
            }

            return "Usuário não é um Aluno autenticado";
        }

        // Método para verificar se o usuário autenticado é um Aluno
        private bool IsAuthenticatedAluno(ClaimsPrincipal user)
        {
            return user.IsInRole("aluno");
        }
    }
}
