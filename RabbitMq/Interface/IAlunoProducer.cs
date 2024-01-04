using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Interface
{
    public interface IAlunoProducer
    {
        void EnviarMensagemAluno(Aluno aluno);
        void EnviarMensagemAtualizacaoAluno(Aluno aluno);
        void EnviarMensagemRemocaoAluno(int alunoId);
    }
}
