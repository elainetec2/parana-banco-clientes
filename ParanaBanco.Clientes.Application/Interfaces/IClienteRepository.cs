using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Domain.Entities;


namespace ParanaBanco.Clientes.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default);

        Task<Cliente?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteCpfAsync(
            string cpf,
            CancellationToken cancellationToken = default);
    }
}
