﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Professor
    {
        public int IdProfessor { get; set; }

        public string Nome { get; set; }

        public string Endereço { get; set; }

        public int Idade { get; set; }

        public string Salário { get; set; }

        public bool ProfessorCoordenador { get; set; }

        public List<Disciplina> Disciplinas { get; set; }

    }
}
