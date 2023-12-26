using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DisciplinaDto
    {
        public string Nome { get; set; }
        public int Nota { get; set; }
        public CursoDto Curso { get; set; }
        public ProfessorDto Professor { get; set; }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
    }

    public class ProfessorDto
    {
        public string Nome { get; set; }
    }

    public class DisciplinaProfessorDto
    {
        public int IdCurso { get; set; }

        public int IdDisciplina { get; set; }

        public Curso Curso { get; set; }

        public Disciplina Disciplina { get; set; }

    }

}
