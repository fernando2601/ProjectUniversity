using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
        public class AlunoCursoDTO
        {
            public int Id { get; set; }
            public int IdAluno { get; set; }
            public Aluno Aluno { get; set; }
            public int IdCurso { get; set; }
            public Curso Curso { get; set; }
            public DateTime DataMatricula { get; set; }
            public bool StatusMatricula { get; set; }
        }

 }

