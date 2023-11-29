using System;
using System.Collections.Generic;

namespace Domain
{
    public class Curso
    {
        public Guid IdCurso { get; set; }

        public int Semestres { get; set; }

        public string Nome { get; set; }

        public List<Disciplina> Disciplina { get; set; }

        public int Mensalidade { get; set; }


    }
}
