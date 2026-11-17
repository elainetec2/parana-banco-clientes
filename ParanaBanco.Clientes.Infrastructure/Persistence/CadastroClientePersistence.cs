using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ParanaBanco.Clientes.Application.Events;
using ParanaBanco.Clientes.Application.Interfaces;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.Infrastructure.Persistence
{
    public sealed class CadastroClientePersistence
    : ICadastroClientePersistence
    {
        private readonly ClientesDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public CadastroClientePersistence(
            ClientesDbContext dbContext,
            IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task SalvarClienteComEventoAsync(
            Cliente cliente,
            ClienteCadastrado evento,
            CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync(
                    cancellationToken);

            await _dbContext.Clientes.AddAsync(
                cliente,
                cancellationToken);

            await _publishEndpoint.Publish(
                evento,
                cancellationToken);

            await _dbContext.SaveChangesAsync(
                cancellationToken);

            await transaction.CommitAsync(
                cancellationToken);
        }
    }
}
