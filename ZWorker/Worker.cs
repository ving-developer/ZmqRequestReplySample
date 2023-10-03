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

        
        using var server = new ResponseSocket();

        // Se conecta ao Dealer presente no nosso ZBroker
        server.Connect("tcp://127.0.0.1:5560");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Server awaiting for connections");

            // Chama uma função bloqueante que irá aguardar pelo próximo frame de dados recebido pelo Dealer
            var request = server.ReceiveFrameString();

            _logger.LogInformation($"Received {request}");

            // Chama um método que recebe uma string de conteudo e devolve um pdf em bytes
            var response = GeneratePdf(request);

            // Retorna os bytes do PDF para o Dealer
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