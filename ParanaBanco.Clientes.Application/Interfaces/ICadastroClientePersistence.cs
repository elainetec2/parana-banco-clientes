using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Application.Events;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.Application.Interfaces
{
    public interface ICadastroClientePersistence
    {
        Task SalvarClienteComEventoAsync(
            Cliente cliente,
            ClienteCadastrado evento,
            CancellationToken cancellationToken = default);
    }
}
