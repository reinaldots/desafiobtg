using DesafioBTG.API.Models;
using DesafioBTG.API.Services;
using DesafioBTG.API.Messaging;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _service;
    private readonly IConfiguration _configuration;

    public PedidosController(PedidoService service, 
        IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }

    [HttpGet()]
    public IActionResult Ping()
    {
        return Ok("API está funcionando!");
    }

    [HttpPost("pedidos")]
    public IActionResult CriarPedido([FromBody] Pedido pedido)
    {
        if (pedido == null)
            return BadRequest("Pedido inválido.");

        try
        {
            var rabbitSettings = _configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

            if (rabbitSettings == null || string.IsNullOrWhiteSpace(rabbitSettings.HostName))
                throw new InvalidOperationException("Parâmetro RabbitMQ não encontrado.");

            var publisher = new PedidoPublisher(rabbitSettings);

            publisher.IncluirPedido(pedido);
            return Accepted($"Pedido {pedido.CodigoPedido} salvo na fila com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao salvar pedido na fila: {ex.Message}");
        }
    }

    [HttpGet("pedidos/{codigoCliente}")]
    public async Task<IActionResult> ListarPedidosPorCliente(int codigoCliente)
    {
        var pedidos = await _service.ListarPedidosPorCliente(codigoCliente);

        if (pedidos.Count == 0) return NotFound("Nenhum pedido foi encontrado.");
        
        return Ok(pedidos);
    }

    [HttpGet("pedidos/{codigoCliente}/quantidade")]
    public async Task<IActionResult> ObterQuantidadePedidos(int codigoCliente)
    {
        var quantidade = await _service.ObterQuantidadePedido(codigoCliente);
        return Ok(quantidade);
    }

    [HttpGet("pedidos/{codigoPedido}/valortotal")]
    public async Task<IActionResult> ObterValorTotal(int codigoPedido)
    {
        var total = await _service.ObterValorTotalDoPedido(codigoPedido);

        if (total == 0) return NotFound("Nenhum pedido foi encontrado.");

        return Ok(total);
    }

}
