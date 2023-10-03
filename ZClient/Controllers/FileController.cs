using Microsoft.AspNetCore.Mvc;
using NetMQ;
using NetMQ.Sockets;

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
        public IActionResult GetPdf([FromBody] string content)
        {
            _logger.LogDebug($"Executing file/pdf with content:\n{content}");

            using (var client = new RequestSocket(">tcp://localhost:5559"))
            {
                client.SendFrame(content);

                var response = client.ReceiveFrameString();

                _logger.LogDebug($"Finish file/pdf with content:\n{content}");

                return Ok(response);
            }

            return null;
        }
    }
}