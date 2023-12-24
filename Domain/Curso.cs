using System;
using System.Collections.Generic;

namespace Domain
{
    public class Curso
    {
        public int IdCurso { get; set; }

        public int Semestres { get; set; }

        public string Nome { get; set; }

        public List<Disciplina> Disciplina { get; set; }

        public int Mensalidade { get; set; }

        public List<AlunoCursoDTO> CursosAlunos { get; set; }  // Lista de alunos associados ao curso


    }
}
