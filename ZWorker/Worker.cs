using NetMQ;
using NetMQ.Sockets;

namespace ZWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"Starting server listen to tcp://localhost:5560");

        //
        // Hello World worker
        // Connects REP socket to tcp://127.0.0.1:5560
        // Expects "Hello" from client, replies with "World"
        //
        // Author: metadings
        //

        // Socket to talk to clients

        using var server = new ResponseSocket();

        server.Connect("tcp://127.0.0.1:5560");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Server awaiting for connections");

            // Wait for next request from client
            var request = server.ReceiveFrameString();

            _logger.LogInformation($"Received {request}");

            // Do some 'work'
            //Thread.Sleep(1);

            // Send reply back to client
            server.SendFrame($"Hi Back {request}");
        }
    }
}