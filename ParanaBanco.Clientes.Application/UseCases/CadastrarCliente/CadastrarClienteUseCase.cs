using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Application.DTOs;
using ParanaBanco.Clientes.Application.Events;
using ParanaBanco.Clientes.Application.Interfaces;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.Application.UseCases.CadastrarCliente
{
    public sealed class CadastrarClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICadastroClientePersistence _persistence;

        public CadastrarClienteUseCase(
            IClienteRepository clienteRepository,
            ICadastroClientePersistence persistence)
        {
            _clienteRepository = clienteRepository;
            _persistence = persistence;
        }

        public async Task<ClienteResponse> ExecutarAsync(
            CadastrarClienteRequest request,
            CancellationToken cancellationToken = default)
        {
            var cliente = new Cliente(
                request.Nome,
                request.Cpf,
                request.Email);

            var cpfExistente = await _clienteRepository.ExisteCpfAsync(
                cliente.Cpf.Valor,
                cancellationToken);

            if (cpfExistente)
                throw new InvalidOperationException(
                    "Já existe um cliente cadastrado com este CPF.");

            var evento = new ClienteCadastrado(
                Guid.NewGuid(),
                cliente.Id,
                cliente.Nome,
                cliente.Cpf.Valor,
                cliente.Email.Valor,
                DateTime.UtcNow);

            await _persistence.SalvarClienteComEventoAsync(
                cliente,
                evento,
                cancellationToken);

            return new ClienteResponse(
                cliente.Id,
                cliente.Nome,
                cliente.Cpf.Valor,
                cliente.Email.Valor,
                cliente.CriadoEm);
        }
    }
}
