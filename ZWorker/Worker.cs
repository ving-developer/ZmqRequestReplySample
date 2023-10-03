using iTextSharp.text.pdf;
using iTextSharp.text;
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

            var response = GeneratePdf(request);

            // Send reply back to client
            server.SendFrame(response);
        }
    }

    private byte[] GeneratePdf(string content)
    {
        using MemoryStream ms = new MemoryStream();
        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, ms);
        document.Open();
        document.Add(new Paragraph(content));
        document.Close();
        return ms.ToArray();
    }
}