using Microsoft.AspNetCore.Mvc;
using ZeroMQ;

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
        public IEnumerable<IActionResult> GetPdf([FromBody]string content)
        {
            //
            // Hello World client
            // Connects REQ socket to tcp://127.0.0.1:5559
            // Sends "Hello" to server, expects "World" back
            //
            // Author: metadings
            //

            // Socket to talk to server
            using (var context = new ZContext())
            using (var requester = new ZSocket(context, ZSocketType.REQ))
            {
                requester.Connect("tcp://127.0.0.1:5559");

                for (int n = 0; n < 10; ++n)
                {
                    requester.Send(new ZFrame(content));

                    using (ZFrame reply = requester.ReceiveFrame())
                    {
                        Console.WriteLine("Hello {0}!", reply.ReadString());
                    }
                }
            }

            return null;
        }
    }
}