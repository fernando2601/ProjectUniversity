using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Disciplina
    {
        public Guid Id { get; set; }

        public int Nota { get; set; }

        public int Nome { get; set; }

        public Professor Professores { get; set; }

    }
}
