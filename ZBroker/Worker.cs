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
            //
            // Simple request-reply broker

            var frontend = new RouterSocket("@tcp://127.0.0.1:5559");
            var backend = new DealerSocket("@tcp://127.0.0.1:5560");

            _logger.LogInformation("Listen Router in tcp://127.0.0.1:5559");
            _logger.LogInformation("Listen Dealer in tcp://127.0.0.1:5560");

            // Handler for messages coming in to the frontend
            frontend.ReceiveReady += (s, e) =>
            {
                var msg = e.Socket.ReceiveMultipartMessage();
                backend.SendMultipartMessage(msg); // Relay this message to the backend
            };

            // Handler for messages coming in to the backend
            backend.ReceiveReady += (s, e) =>
            {
                var msg = e.Socket.ReceiveMultipartMessage();
                frontend.SendMultipartMessage(msg); // Relay this message to the frontend
            };

            using var poller = new NetMQPoller { backend, frontend };

            // Listen out for events on both sockets and raise events when messages come in
            poller.Run();
        }
    }
}