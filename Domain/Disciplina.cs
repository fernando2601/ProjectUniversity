using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Disciplina
    {
        public int IdDisciplina { get; set; }

        public int Nota { get; set; }

        public string Nome { get; set; }

        public int IdCurso { get; set; }
        public Curso Curso { get; set; }

        public int IdProfessor { get; set; }
        public Professor Professor { get; set; }

    }
}
