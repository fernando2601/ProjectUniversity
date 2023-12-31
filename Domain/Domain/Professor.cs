using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProfessor { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public int Idade { get; set; }

        public string Salario { get; set; }

        public bool ProfessorCoordenador { get; set; }

        public List<Disciplina> Disciplinas { get; set; }

    }
}
