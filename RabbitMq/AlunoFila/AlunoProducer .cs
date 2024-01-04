using Domain;
using RabbitMq.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.AlunoFila
{
  
    public class AlunoProducer : IAlunoProducer
    {
        // Lógica para enviar a mensagem do aluno para o RabbitMQ
        public void EnviarMensagemAluno(Aluno aluno)
        {
            // Exemplo de lógica para envio de mensagem para o RabbitMQ
            Console.WriteLine($"Enviando mensagem para o RabbitMQ: Novo aluno {aluno.Nome}");

            // Aqui você faria a lógica real para enviar a mensagem para o RabbitMQ.
        }

        // Lógica para enviar a mensagem de atualização do aluno para o RabbitMQ
        public void EnviarMensagemAtualizacaoAluno(Aluno aluno)
        {
            // Exemplo de lógica para envio de mensagem para o RabbitMQ
            Console.WriteLine($"Enviando mensagem para o RabbitMQ: Aluno atualizado {aluno.Nome}");

            // Aqui você faria a lógica real para enviar a mensagem para o RabbitMQ.
        }

        // Lógica para enviar a mensagem de remoção do aluno para o RabbitMQ
        public void EnviarMensagemRemocaoAluno(int alunoId)
        {
            // Exemplo de lógica para envio de mensagem para o RabbitMQ
            Console.WriteLine($"Enviando mensagem para o RabbitMQ: Aluno removido ID {alunoId}");

            // Aqui você faria a lógica real para enviar a mensagem para o RabbitMQ.
        }
    }
}
