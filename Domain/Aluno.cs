using System;
using System.Collections.Generic;

namespace Domain
{
    public class Aluno
    {
        public Guid Id { get; set; }

        public int Nota { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public string Endereço  { get; set; }

        public string Mensalidade { get; set; }

        public string Semestre { get; set; }

        public List<Curso> Curso { get; set; }


    }
}
