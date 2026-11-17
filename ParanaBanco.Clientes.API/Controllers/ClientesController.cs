using Microsoft.AspNetCore.Mvc;
using ParanaBanco.Clientes.Application.DTOs;
using ParanaBanco.Clientes.Application.UseCases.CadastrarCliente;

namespace ParanaBanco.Clientes.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public sealed class ClientesController : ControllerBase
    {
        private readonly CadastrarClienteUseCase _cadastrarCliente;

        public ClientesController(
            CadastrarClienteUseCase cadastrarCliente)
        {
            _cadastrarCliente = cadastrarCliente;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(ClienteResponse),
            StatusCodes.Status201Created)]
        public async Task<ActionResult<ClienteResponse>> Cadastrar(
            [FromBody] CadastrarClienteRequest request,
            CancellationToken cancellationToken)
        {
            var cliente = await _cadastrarCliente.ExecutarAsync(
                request,
                cancellationToken);

            return StatusCode(
                StatusCodes.Status201Created,
                cliente);
        }
    }
}
