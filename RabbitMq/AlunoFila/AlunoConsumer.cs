using Domain;
using RabbitMq.Interface;
using System;


namespace RabbitMq.AlunoFila
{
    public class AlunoConsumer : IAlunoConsumer
    {
        // Lógica para consumir a mensagem do aluno recebida do RabbitMQ
        public void ConsumirMensagemAluno(Aluno aluno)
        {
            // Exemplo de lógica personalizada:
            Console.WriteLine($"Mensagem recebida para o aluno {aluno.Nome}. Iniciando consumo...");

            // Lógica específica aqui, como processar dados, realizar cálculos, etc.

            Console.WriteLine($"Consumo concluído para o aluno {aluno.Nome}.");

            // Se necessário, você pode chamar outros métodos ou serviços a partir daqui.
        }

        // Lógica para consumir a mensagem de atualização do aluno recebida do RabbitMQ
        public void ConsumirMensagemAtualizacaoAluno(Aluno aluno)
        {
            // Lógica para consumir a mensagem de atualização do aluno
            Console.WriteLine($"Mensagem de atualização recebida para o aluno {aluno.Nome}. Iniciando consumo...");

            // Lógica específica para atualização aqui.

            Console.WriteLine($"Consumo concluído para a mensagem de atualização do aluno {aluno.Nome}.");
        }

        // Lógica para consumir a mensagem de remoção do aluno recebida do RabbitMQ
        public void ConsumirMensagemRemocaoAluno(int alunoId)
        {
            // Lógica para consumir a mensagem de remoção do aluno
            Console.WriteLine($"Mensagem de remoção recebida para o aluno com ID {alunoId}. Iniciando consumo...");

            // Lógica específica para remoção aqui.

            Console.WriteLine($"Consumo concluído para a mensagem de remoção do aluno com ID {alunoId}.");
        }
    }
}