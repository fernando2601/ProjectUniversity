using RabbitMq.Interface;
using System;
using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace RabbitMq.AlunoFila
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConnection _connection;
        private readonly string _queueName = "FilaAluno";


        public RabbitMQService(IConnection connection, string queueName)
        {
            _connection = connection;
            _queueName = queueName;
        }

        public void ConsumirMensagem()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var encoding = Encoding.UTF8;
                    var message = encoding.GetString(body.ToArray());

                    Console.WriteLine($"Mensagem recebida: {message}");

                    // Lógica específica para processar a mensagem recebida
                    ProcessarMensagemAluno(message);
                };

                channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            }
        }

        public void EnviarMensagem(string mensagem)
        {
            using (var channel = _connection.CreateModel())
            {
                // Declaração da fila
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                // Codificação da mensagem
                var encoding = Encoding.UTF8;
                var body = encoding.GetBytes(mensagem);

                // Publicação da mensagem na fila
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

                Console.WriteLine($"Mensagem enviada para a fila: {mensagem}");
            }
        }

            private void ProcessarMensagemAluno(string mensagem)
        {
            // Lógica para processar a mensagem do aluno recebida do RabbitMQ
            Console.WriteLine($"Processando mensagem do aluno: {mensagem}");
            // Adicione sua lógica de processamento aqui...
        }
    }
}
