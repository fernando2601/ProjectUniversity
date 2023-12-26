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
        // Outras propriedades necessárias
    }

    public class ProfessorDto
    {
        public string Nome { get; set; }
        // Outras propriedades necessárias
    }

}
