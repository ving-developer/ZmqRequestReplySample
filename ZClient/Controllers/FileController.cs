using Microsoft.AspNetCore.Mvc;
using NetMQ;
using NetMQ.Sockets;
using System;

namespace ZClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpPost("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        public IActionResult GetPdf([FromBody] string content)
        {
            _logger.LogDebug($"Executing file/pdf with content:\n{content}");

            using (var client = new RequestSocket(">tcp://localhost:5559"))// Aqui estamos fazendo um connect ao Router, na porta 5559
            {
                // O método send irá enviar um conjunto de frames com o conteúdo especificado em content
                client.SendFrame(content);

                // Aqui o client irá chamar uma função bloqueante, aguardando uma resposta do servidor
                var response = client.ReceiveFrameBytes();

                _logger.LogDebug($"Finish file/pdf with content:\n{ content }");

                return File(response, "application/octet-stream", $"{ (new Random()).Next(0, 10001) }.pdf");
            }

            return null;
        }
    }
}