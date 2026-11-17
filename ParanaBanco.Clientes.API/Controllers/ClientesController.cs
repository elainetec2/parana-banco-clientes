using Microsoft.AspNetCore.Mvc;
using ParanaBanco.Clientes.Application.DTOs;
using ParanaBanco.Clientes.Application.Exceptions;
using ParanaBanco.Clientes.Application.UseCases.CadastrarCliente;
using ParanaBanco.Clientes.Application.UseCases.ObterClientePorId;


namespace ParanaBanco.Clientes.API.Controllers
{
    [ApiController]
    [Route("api/v1/clientes")]
    public sealed class ClientesController : ControllerBase
    {
        private readonly CadastrarClienteUseCase _cadastrarCliente;
        private readonly ObterClientePorIdUseCase _obterClientePorId;

        public ClientesController(
            CadastrarClienteUseCase cadastrarCliente,
            ObterClientePorIdUseCase obterClientePorId)
        {
            _cadastrarCliente = cadastrarCliente;
            _obterClientePorId = obterClientePorId;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(ClienteResponse),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ClienteResponse>> Cadastrar(
            [FromBody] CadastrarClienteRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _cadastrarCliente.ExecutarAsync(
                    request,
                    cancellationToken);

                return StatusCode(
                    StatusCodes.Status201Created,
                    cliente);
            }
            catch (ClienteJaCadastradoException ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(
            typeof(ClienteResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteResponse>> ObterPorId(
            Guid id,
            CancellationToken cancellationToken)
        {
            var cliente = await _obterClientePorId.ExecutarAsync(
                id,
                cancellationToken);

            if (cliente is null)
                return NotFound();

            return Ok(cliente);
        }

    }
}
