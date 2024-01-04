using Domain;


namespace RabbitMq.Interface
{
    public interface IAlunoConsumer
    {
        void ConsumirMensagemAluno(Aluno aluno);
        void ConsumirMensagemAtualizacaoAluno(Aluno aluno);
        void ConsumirMensagemRemocaoAluno(int alunoId);
    }
}
