using NetMQ;
using NetMQ.Sockets;

namespace ZBroker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var frontend = new RouterSocket("@tcp://127.0.0.1:5559");// Faz um bind do Router na porta 5559, aguardando por conexões do cliente
            var backend = new DealerSocket("@tcp://127.0.0.1:5560");// Faz um bind do Dealer na porta 5560, aguardando por conexões do servidor

            _logger.LogInformation("Listen Router in tcp://127.0.0.1:5559");
            _logger.LogInformation("Listen Dealer in tcp://127.0.0.1:5560");

            // Atualiza o evento que será chamado internamente quando uma mensagem do cliente chegar no Router
            frontend.ReceiveReady += (s, e) =>
            {
                var msg = e.Socket.ReceiveMultipartMessage();// Recebe a mensagem vinda do cliente
                backend.SendMultipartMessage(msg); // Retransmite ela ao servidor
            };

            // Atualiza o evento que será chamado internamente quando uma mensagem do servidor chegar no Dealer
            backend.ReceiveReady += (s, e) =>
            {
                var msg = e.Socket.ReceiveMultipartMessage();// Recebe a mensagem vinda do servidor
                frontend.SendMultipartMessage(msg); // Retransmite ela ao cliente
            };

            //Cria uma instancia de NetMQPoller, que possibilita ouvir vários eventos. Para nosso caso o Router e o Dealer
            using var poller = new NetMQPoller { backend, frontend };
            
            poller.Run();// Cria thread ficará ocupada escutando os eventos no Router e Dealer, chamando o ReceiveReady correspondente (configurado anteriormente)
        }
    }
}