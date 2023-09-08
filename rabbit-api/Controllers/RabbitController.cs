using Microsoft.AspNetCore.Mvc;
using rabbit_api.Services;

namespace Rabbit_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RabbitController : ControllerBase
{
    private class MessageDto
    {
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.Now;
    }
    
    private readonly IMessageProducer _producer;

    public RabbitController(IMessageProducer producer)
    {
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
    }

    [HttpPost]
    public IActionResult SendMessage([FromBody] string name)
    {
        var mensagem = new MessageDto { Name = name };
        _producer.SendMessage(mensagem);
        return Ok();
    }
}