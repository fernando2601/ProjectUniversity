using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Aluno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAluno { get; set; }

        public int Nota { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public string Endereco  { get; set; }

        public string Mensalidade { get; set; }

        public string Semestre { get; set; }

        public List<AlunoCursoDTO> CursosAlunos { get; set; }  // Lista de cursos associados ao aluno

    }
}
