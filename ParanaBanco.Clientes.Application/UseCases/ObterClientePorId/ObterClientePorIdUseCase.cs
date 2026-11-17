using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Application.DTOs;
using ParanaBanco.Clientes.Application.Interfaces;

namespace ParanaBanco.Clientes.Application.UseCases.ObterClientePorId
{    public sealed class ObterClientePorIdUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientePorIdUseCase(
            IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse?> ExecutarAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(
                id,
                cancellationToken);

            if (cliente is null)
                return null;

            return new ClienteResponse(
                cliente.Id,
                cliente.Nome,
                cliente.Cpf.Valor,
                cliente.Email.Valor,
                cliente.CriadoEm);
        }
    }
}
