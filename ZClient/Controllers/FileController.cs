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

            using (var client = new RequestSocket(">tcp://localhost:5559"))
            {
                client.SendFrame(content);

                var response = client.ReceiveFrameBytes();

                _logger.LogDebug($"Finish file/pdf with content:\n{content}");

                return File(response, "application/octet-stream", $"{ (new Random()).Next(0, 10001) }.pdf");
            }

            return null;
        }
    }
}