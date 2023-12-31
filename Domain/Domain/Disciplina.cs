using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Disciplina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDisciplina { get; set; }

        public int Nota { get; set; }

        public string Nome { get; set; }

        public int IdCurso { get; set; }
        public Curso Curso { get; set; }

        public int IdProfessor { get; set; }
        public Professor Professor { get; set; }

    }
}
