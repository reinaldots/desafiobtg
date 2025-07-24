using DesafioBTG.API.Models;
using DesafioBTG.API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _service;

    public PedidosController(PedidoService service)
    {
        _service = service;
    }

    [HttpGet()]
    public IActionResult Ping()
    {
        return Ok("API está funcionando!");
    }

    [HttpPost("pedidos")]
    public async Task<IActionResult> CriarPedido([FromBody] Pedido pedido)
    {
        //incluir pedido no MongoDB e retornar registro na API
        await _service.InserirPedido(pedido);
        return CreatedAtAction(nameof(ListarPedidosPorCliente), new { codigoCliente = pedido.CodigoCliente }, pedido);
    }

    [HttpGet("pedidos/{codigoCliente}")]
    public async Task<IActionResult> ListarPedidosPorCliente(int codigoCliente)
    {
        var pedidos = await _service.ListarPedidosPorCliente(codigoCliente);
        return Ok(pedidos);
    }

}
