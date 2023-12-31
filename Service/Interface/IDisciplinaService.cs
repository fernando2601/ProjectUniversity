﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDisciplinaService
    {
        Task<IEnumerable<Disciplina>> ObterTodasDisciplinasAsync();
        Task<Disciplina> ObterDisciplinaPorIdAsync(int id);
        Task<int> AdicionarDisciplinaoAsync(DisciplinaDto disciplina);
        Task<bool> AtualizarDisciplinaAsync(Disciplina disciplina);
        Task<bool> DeletarDisciplinaAsync(int IdDisciplina);
    }
}
